using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EncryptionDemo.EncryptionEngine;

namespace EncryptionDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"csharp 8 encryption");
            Console.WriteLine(Environment.NewLine);
            var plain = args.Length > 0 ? args[0] : "This is the default string to encrypt if nothing is passed in.";
            Console.WriteLine($"PlainText: {plain}");
            Console.WriteLine(Environment.NewLine);
            RunAes(plain);
            RunRijndael(plain);
            await RunAesAsync(plain);
            await RunRijndaelAsync(plain);
            Console.ReadKey();
        }

        private static void RunAes(string plain)
        {
            using var cryptKeeper = new AesCryptKeeper();
            var aesEncrypted = cryptKeeper.Encrypt(plain);
            var aesDecrypted = cryptKeeper.Decrypt(aesEncrypted);
            Console.WriteLine("-- Aes Sync --");
            Console.WriteLine($"Encrypted: {aesEncrypted}");
            Console.WriteLine($"Decrypted: {aesDecrypted}");
            Console.WriteLine(Environment.NewLine);
        }

        private static void RunRijndael(string plain)
        {
            using var cryptKeeper = new RijndaelCryptKeeper();
            var rijndaelEncrypted = cryptKeeper.Encrypt(plain);
            var rijndaelDecrypted = cryptKeeper.Decrypt(rijndaelEncrypted);
            Console.WriteLine("-- Rijndael Sync --");
            Console.WriteLine($"Encrypted: {rijndaelEncrypted}");
            Console.WriteLine($"Decrypted: {rijndaelDecrypted}");
            Console.WriteLine(Environment.NewLine);
        }

        private static async Task RunAesAsync(string plain)
        {
            using var cryptKeeper = new AesCryptKeeper();
            var aesEncryptedAsync = await cryptKeeper.EncryptAsync(plain);
            var aesDecryptedAsync = await cryptKeeper.DecryptAsync(aesEncryptedAsync);
            Console.WriteLine("-- Aes Async --");
            Console.WriteLine($"Encrypted: {aesEncryptedAsync}");
            Console.WriteLine($"Decrypted: {aesDecryptedAsync}");
            Console.WriteLine(Environment.NewLine);
        }

        private static async Task RunRijndaelAsync(string plain)
        {
            using var cryptKeeper = new RijndaelCryptKeeper();
            var rijndaelEncryptedAsync = await cryptKeeper.EncryptAsync(plain);
            var rijndaelDecryptedAsync = await cryptKeeper.DecryptAsync(rijndaelEncryptedAsync);
            Console.WriteLine("-- Rijndael Async --");
            Console.WriteLine($"Encrypted: {rijndaelEncryptedAsync}");
            Console.WriteLine($"Decrypted: {rijndaelDecryptedAsync}");
            Console.WriteLine(Environment.NewLine);
        }
    }
}
