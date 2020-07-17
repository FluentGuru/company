using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Company.Domain.Exceptions
{
    public class IsinExistsException : Exception
    {
        public IsinExistsException()
        {
        }

        public IsinExistsException(string message) : base(message)
        {
        }

        public IsinExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IsinExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
