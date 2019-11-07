using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDemo.EncryptionEngine
{
    /// <summary>
    /// Rijndael Wrapper.
    /// Key Size: 256
    /// Block Size: 128
    /// </summary>
    internal sealed class RijndaelCryptKeeper : ISymmetricCryptKeeper
    {
        private const short KeySize = 256;
        private byte[] _key;
        private byte[] _iv;

        internal RijndaelCryptKeeper()
        {
            HandleKeys();
        }

        /// <summary>
        /// Gets secrets from store and sets secret fields.
        /// </summary>
        //!TODO: Store/Retrieve secrets in key vault here
        private void HandleKeys()
        {
            using var rijndael = new RijndaelManaged
            {
                KeySize = KeySize
            };
            _key = rijndael.Key;
            _iv = rijndael.IV;
        }

        /// <summary>
        /// Create a properly configured RijndaelManaged encryptor.
        /// </summary>
        /// <returns>Configured encryptor.</returns>
        public ICryptoTransform CreateEncryptor()
        {
            using var rijndael = new RijndaelManaged
            {
                KeySize = KeySize,
                Key = _key,
                IV = _iv,
            };
            return rijndael.CreateEncryptor(_key, _iv);
        }

        /// <summary>
        /// Create a properly configured AesManaged decryptor.
        /// </summary>
        /// <returns>Configured decryptor.</returns>
        public ICryptoTransform CreateDecryptor()
        {
            using var aes = new AesManaged
            {
                Key = _key,
                IV = _iv
            };
            return aes.CreateDecryptor(_key, _iv);
        }         


        /// <summary>
        /// Encrypt string.
        /// </summary>
        /// <param name="plain">Plain text value.</param>
        /// <returns>Encrypted value.</returns>
        public string Encrypt(string plain)
        {
            using var encryptor = CreateEncryptor();
            using var memStream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
            {
                using var streamWriter = new StreamWriter(cryptoStream);
                streamWriter.Write(plain);
            }
            var crypticBytes = memStream.ToArray();
            return Convert.ToBase64String(crypticBytes);
        }

        /// <summary>
        /// Decrypt string.
        /// </summary>
        /// <param name="encrypted">Encrypted value.</param>
        /// <returns>Plain text value.</returns>
        public string Decrypt(string encrypted)
        {
            using var decryptor = CreateDecryptor();
            using var memStream = new MemoryStream(Convert.FromBase64String(encrypted));
            using var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);
            string decrypted;
            using (var streamReader = new StreamReader(cryptoStream))
            {
                decrypted = streamReader.ReadToEnd();
            }
            return decrypted;
        }

        /// <summary>
        /// Encrypt string async.
        /// </summary>
        /// <param name="plain">Plain text value.</param>
        /// <returns>Encrypted value.</returns>
        public async Task<string> EncryptAsync(string plain)
        {
            using var encryptor = CreateEncryptor();
            await using var memStream = new MemoryStream();
            await using (var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
            {
                await using var streamWriter = new StreamWriter(cryptoStream);
                await streamWriter.WriteAsync(plain);
            }
            var crypticBytes = memStream.ToArray();
            return Convert.ToBase64String(crypticBytes);
        }

        /// <summary>
        /// Decrypt string async.
        /// </summary>
        /// <param name="encrypted">Encrypted value.</param>
        /// <returns>Plain text value.</returns>
        public async Task<string> DecryptAsync(string encrypted)
        {
            using var decryptor = CreateDecryptor();
            await using var memStream = new MemoryStream(Convert.FromBase64String(encrypted));
            await using var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);
            string decrypted;
            using (var streamReader = new StreamReader(cryptoStream))
            {
                decrypted = await streamReader.ReadToEndAsync();
            }
            return decrypted;
        }

        /// <summary>
        /// Implements disposing so these classes can be instantiated with using blocks.
        /// </summary>
        /// <param name="disposing">follows dotnet dispose pattern.</param>
        private void Dispose(bool disposing)
        {
            if (_key != null) Array.Clear(_key, 0, _key.Length);
            if (_iv != null) Array.Clear(_iv, 0, _iv.Length);
        }

        /// <summary>
        /// Implements disposing so these classes can be instantiated with using blocks.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
