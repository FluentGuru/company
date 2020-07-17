using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Company.Domain.Exceptions
{
    public class IsbnExistsException : Exception
    {
        public IsbnExistsException()
        {
        }

        public IsbnExistsException(string message) : base(message)
        {
        }

        public IsbnExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IsbnExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
