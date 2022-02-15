using System.Security.Cryptography;
using System.Text;

namespace Trails.Security
{
    public static class SecurityProvider
    {
        public static string RandomBeaconKeyGenerator()
        {
            const int keySize = 32;
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#$%^-+";

            char[] chars = characters.ToCharArray();
            byte[] data = new byte[keySize];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(keySize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static string KeyHasher(string key)
        {
            using var hash = SHA256.Create();
            byte[] bytes = hash
                .ComputeHash(Encoding.UTF8.GetBytes(key));

            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
