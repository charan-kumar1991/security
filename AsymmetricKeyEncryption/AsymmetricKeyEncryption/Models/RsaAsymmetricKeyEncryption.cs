using AsymmetricKeyEncryption.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsymmetricKeyEncryption.Models
{
    public class RsaAsymmetricKeyEncryption : IAsymmetricKeyEncryption
    {
        private readonly RSA _privateKey;
        private readonly RSA _publicKey;

        public RsaAsymmetricKeyEncryption()
        {
            string privateKeyFile = File.ReadAllText(@"D:\security\AsymmetricKeyEncryption\AsymmetricKeyEncryption\Keys\private_key.pem");
            string publicKeyFile = File.ReadAllText(@"D:\security\AsymmetricKeyEncryption\AsymmetricKeyEncryption\Keys\public_key.pem");

            _privateKey = RSA.Create();
            _privateKey.ImportFromPem(privateKeyFile.ToCharArray());


            _publicKey = RSA.Create();
            _publicKey.ImportFromPem(publicKeyFile.ToCharArray());
        }


        public string DecryptText(string encrypted)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encrypted);
            byte[] decryptedBytes = _privateKey.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        }

        public string EncryptText(string text)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = _publicKey.Encrypt(plainTextBytes, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
