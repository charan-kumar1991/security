using Hashing.Interfaces;
using Hashing.Models;
using System;

namespace Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello World!";
            IHasher shaHasher = new ShaHasher();
            string hashed1 = shaHasher.Hash(text);

            IHasher bcryptHasher = new BCryptHasher();
            string hashed2 = bcryptHasher.Hash(text);
        }
    }
}
