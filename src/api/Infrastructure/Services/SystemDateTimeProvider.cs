using Company.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Infrastructure.Services
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}
