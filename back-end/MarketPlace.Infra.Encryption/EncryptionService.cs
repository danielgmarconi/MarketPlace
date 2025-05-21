using System.Security.Cryptography;
using System.Text;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Infra.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly string _secretkey;
        public EncryptionService(string secretkey)
        {
            _secretkey = secretkey;
        }
        public bool Valid(string encryptedValue, string comparedValue)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(_secretkey.PadRight(32));
            aesAlg.IV = new byte[16];

            using var decryptor = aesAlg.CreateDecryptor();
            byte[] cipherBytes = Convert.FromBase64String(encryptedValue);
            byte[] decrypted = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(decrypted).Equals(comparedValue);
        }

        public string Encrypt(string value)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(_secretkey.PadRight(32));
            aesAlg.IV = new byte[16];
            using var encryptor = aesAlg.CreateEncryptor();
            byte[] inputBuffer = Encoding.UTF8.GetBytes(value);
            byte[] encrypted = encryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(encrypted);
        }
    }
}
