using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services.Password
{
    public static class PasswordService
    {
        public static bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            return HashPassword(password, salt) == hashedPassword;
        }

        public static string HashPassword(string password, string salt)
        {
            using var sha512 = SHA512.Create();
            var inputBytes = Encoding.ASCII.GetBytes(password + salt);
            var hashBytes = sha512.ComputeHash(inputBytes);
            return BytesToHexString(hashBytes);
        }

        public static string GenerateSalt()
        {
            var salt = new byte[32];
            RandomNumberGenerator.Create().GetBytes(salt);
            return BytesToHexString(salt);
        }

        private static string BytesToHexString(IEnumerable<byte> input)
        {
            var sb = new StringBuilder();
            foreach (var t in input)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
