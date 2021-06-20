using HooliCash.IHelpers;
using System;
using System.Security.Cryptography;
using System.Text;

namespace HooliCash.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        public string HashPassword(string password, DateTimeOffset passwordLastUpdatedTime)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var salt = GenerateSalt(passwordLastUpdatedTime);

            var passwordHash = HashPassword(password, salt);

            return passwordHash;
        }

        public string GenerateSalt(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("ddMMyyyyhhmmss");
        }

        private string HashPassword(string password, string salt, int iterations = 100000)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(password);

            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(valueBytes, saltBytes, iterations))
            {
                var hashBytes = rfc2898DeriveBytes.GetBytes(32);

                var hashString = Convert.ToBase64String(hashBytes);

                return hashString;
            }
        }
    }
}
