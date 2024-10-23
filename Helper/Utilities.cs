using System.Security.Cryptography;

namespace InventoryAPI.Helper
{
    public class Utilities
    {
        public static string CreateRandomVerificationToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(8));
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password can't be null or empty");
            }
            else
            {
                var hashPass = BCrypt.Net.BCrypt.HashPassword(password);
                return hashPass;
            }
        }

        public static bool DecryptPassword(string password, string hashpass)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password can't be null or empty");
            }
            return BCrypt.Net.BCrypt.Verify(password, hashpass);

        }

    }
}

