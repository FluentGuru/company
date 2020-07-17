using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Company.Domain.Exceptions
{
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException()
        {
        }

        public CompanyNotFoundException(string message) : base(message)
        {
        }

        public CompanyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CompanyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
