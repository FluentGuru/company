using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
