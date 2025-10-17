using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using tests.Services.IServices;

namespace tests.Services
{
    public class Crypto(IConfiguration config) : ICrypto
    {
        
        public IConfiguration _configuration { get; set; } = config;

        public string Encrypt(string pass, out byte[] saltOut)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128/8);
            saltOut = salt;
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(pass, salt , KeyDerivationPrf.HMACSHA256, 100, 256/8));
        }

        public string Encrypt(string pass, in byte[] saltIn, int _ = 1)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(pass, saltIn, KeyDerivationPrf.HMACSHA256, 100, 256/8));
        }

    }
}
