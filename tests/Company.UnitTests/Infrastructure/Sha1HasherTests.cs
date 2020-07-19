using Company.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Company.UnitTests.Infrastructure
{
    public class Sha1HasherTests
    {
        [Fact]
        public void ShouldGenerateSaltWithSpecifiedLength()
        {
            Assert.Equal(8, new Sha1Hasher().GenerateSalt(8).Length);
        }

        [Fact]
        public void ShouldGenerateHash()
        {
            Assert.Equal("644cce8fe4ea3b4ff7f14840e2d29cbed775b2f9", new Sha1Hasher().GenerateHash("SOURCE", "SALT"));
        }
    }
}
