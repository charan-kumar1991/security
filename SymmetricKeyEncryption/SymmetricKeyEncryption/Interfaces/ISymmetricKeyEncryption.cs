namespace SymmetricKeyEncryption.Interfaces
{
    public interface ISymmetricKeyEncryption
    {
        public string EncryptText(string text);
        public string DecryptText(string encrypted);
    }
}
