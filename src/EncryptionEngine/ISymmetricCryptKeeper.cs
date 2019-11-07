using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDemo.EncryptionEngine
{
    public interface ISymmetricCryptKeeper : IDisposable
    {
        /// <summary>
        /// Create a properly configured encryptor.
        /// </summary>
        /// <returns>Configured encryptor.</returns>
        ICryptoTransform CreateEncryptor();
        /// <summary>
        /// Create a properly configured decryptor.
        /// </summary>
        /// <returns>Configured encryptor.</returns>
        ICryptoTransform CreateDecryptor();
        /// <summary>
        /// Encrypt string.
        /// </summary>
        /// <param name="plain">Plain text value.</param>
        /// <returns>Encrypted value.</returns>
        string Encrypt(string plain);
        /// <summary>
        /// Decrypt string.
        /// </summary>
        /// <param name="encrypted">Encrypted value.</param>
        /// <returns>Plain text value.</returns>
        string Decrypt(string encrypted);
        /// <summary>
        /// Encrypt string async.
        /// </summary>
        /// <param name="plain">Plain text value.</param>
        /// <returns>Encrypted value.</returns>
        Task<string> EncryptAsync(string plain);
        /// <summary>
        /// Decrypt string async.
        /// </summary>
        /// <param name="encrypted">Encrypted value.</param>
        /// <returns>Plain text value.</returns>
        Task<string> DecryptAsync(string encrypted);
    }
}
