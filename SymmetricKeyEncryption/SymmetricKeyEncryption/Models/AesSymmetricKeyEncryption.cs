using SymmetricKeyEncryption.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricKeyEncryption.Models
{
    public class AesSymmetricKeyEncryption : ISymmetricKeyEncryption
    {
        private readonly Aes _cipher;
        public AesSymmetricKeyEncryption()
        {
            // AES algorithm with default
            // auto-generated key and IV
            _cipher = Aes.Create();
            _cipher.Mode = CipherMode.CBC;
            _cipher.Padding = PaddingMode.ISO10126;
        }

        public string DecryptText(string encrypted)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encrypted);
            using (ICryptoTransform transformer = _cipher.CreateDecryptor())
            {
                byte[] decryptedBytes = transformer.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        public string EncryptText(string text)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
            using (ICryptoTransform transformer = _cipher.CreateEncryptor())
            {
                byte[] encryptedBytes = transformer.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }
}
