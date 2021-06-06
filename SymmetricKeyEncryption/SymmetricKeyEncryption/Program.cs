using SymmetricKeyEncryption.Interfaces;
using SymmetricKeyEncryption.Models;
using System;

namespace SymmetricKeyEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello world!!!";
            ISymmetricKeyEncryption i = new AesSymmetricKeyEncryption();
            string encrypted = i.EncryptText(text);

            string decrypted = i.DecryptText(encrypted);
        }
    }
}
