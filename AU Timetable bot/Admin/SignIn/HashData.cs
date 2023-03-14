using System.Security.Cryptography;
using System.Text;

namespace Timetablebot.Admin.SignIn
{
    public class HashData
    {
        public static string GetHashData(string plainText, string salt)
        {
            byte[] hashValue = ComputeHash(plainText, salt);
            return GetStringHashValue(hashValue);
        }

        private static byte[] ComputeHash(string plainText, string salt)
        {
            byte[] plainDataWithSalt = Encoding.UTF8.GetBytes(salt + plainText);
            using HashAlgorithm Sha256 = SHA256.Create();
            return Sha256.ComputeHash(plainDataWithSalt);
        }

        private static string GetStringHashValue(byte[] hashValue)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                sb.Append(hashValue[i].ToString("x"));
            }
            return sb.ToString();
        }
    }
}
