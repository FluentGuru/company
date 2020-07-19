using Company.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Company.UnitTests.Infrastructure
{
    public class SystemDateTimeProviderTests
    {
        [Fact]
        public void ShouldReturnSameAsSystemDate()
        {
            Assert.Equal(DateTime.Now.Date, new SystemDateTimeProvider().Now().Date);
        }
    }
}
