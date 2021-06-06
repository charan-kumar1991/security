using AsymmetricKeyEncryption.Interface;
using AsymmetricKeyEncryption.Models;
using System;

namespace AsymmetricKeyEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello world!!!!";
            IAsymmetricKeyEncryption i = new RsaAsymmetricKeyEncryption();
            string encrypted = i.EncryptText(text);

            string decrypted = i.DecryptText(encrypted);


        }
    }
}
