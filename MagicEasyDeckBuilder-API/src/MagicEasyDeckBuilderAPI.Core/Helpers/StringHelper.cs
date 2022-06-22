using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Core.Helpers
{
    public static class StringHelper
    {
        public static string HashString(string text, string key = "")
        {

            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var textBytes = Encoding.UTF8.GetBytes(text + key);
                var hashByte = sha.ComputeHash(textBytes);

                var hash = BitConverter
                    .ToString(hashByte)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
    }
}
