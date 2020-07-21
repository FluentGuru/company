using Company.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Messages
{
    public class AuthenticateCommand
    {
        public AuthenticateCommand(Credentials credentials)
        {
            Credentials = credentials;
        }

        public Credentials Credentials { get; }
    }
}
