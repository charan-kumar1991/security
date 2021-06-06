namespace AsymmetricKeyEncryption.Interface
{
    public interface IAsymmetricKeyEncryption
    {
        public string EncryptText(string text);
        public string DecryptText(string encrypted);
    }
}
