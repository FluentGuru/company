using Company.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Company.Infrastructure.Services
{
    public class Sha1Hasher : IHasher
    {
        public string GenerateHash(string source, string salt = "")
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(source+salt));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }

        public string GenerateSalt(int length)
        {
            var salt = "";
            var random = new Random();
            for (int i = 0; i < length; i++)
            {
                salt += (char)random.Next(64, 90);
            }

            return salt;
        }
    }
}
