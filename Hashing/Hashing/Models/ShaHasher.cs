using Hashing.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Hashing.Models
{
    public class ShaHasher : IHasher
    {
        public string Hash(string text)
        {
            // Instance of hash function
            SHA512 hasher = SHA512.Create();

            // Convert plain text to byte array
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
           
            byte[] hashedBytes = hasher.ComputeHash(plainTextBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("X2"));
            }

            return builder.ToString();
        }
    }
}
