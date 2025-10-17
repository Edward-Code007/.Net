namespace tests.Services.IServices
{
    public interface ICrypto
    {
        public string Encrypt(string pass, out byte[] saltOut);
        public string Encrypt(string pass, in byte[] saltIn, int _ = 1);
       // public string Decrypt(string decryptString);
    }
}
