using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Core.Helpers
{
    public class PasswordHasher
    {
        private readonly string _hashKey;

        public PasswordHasher(IConfiguration configuration)
        {
            _hashKey = configuration["AppSettings:HashKey"] 
                ?? throw new ArgumentNullException("HashKey configuration is missing");
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + _hashKey));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
} 