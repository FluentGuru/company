using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Services
{
    public interface IHasher
    {
        string GenerateHash(string source, string salt = "");
        string GenerateSalt(int length);
    }
}
