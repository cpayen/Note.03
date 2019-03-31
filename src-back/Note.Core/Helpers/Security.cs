using System;
using System.Security.Cryptography;

namespace Note.Core.Helpers
{
    public static class Security
    {
        public static string GetNewSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string EncryptPassword(string password, string salt)
        {
            var salted = GetSaltedPassword(password, salt);
            return BCrypt.Net.BCrypt.HashPassword(salted);
        }

        public static bool CheckPassword(string password, string salt, string hashedPassword)
        {
            var salted = GetSaltedPassword(password, salt);
            return BCrypt.Net.BCrypt.Verify(salted, hashedPassword);
        }

        private static string GetSaltedPassword(string password, string salt)
        {
            return $"{password}{salt}";
        }
    }
}
