using Hashing.Interfaces;

namespace Hashing.Models
{
    public class BCryptHasher : IHasher
    {
        public string Hash(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }
    }
}
