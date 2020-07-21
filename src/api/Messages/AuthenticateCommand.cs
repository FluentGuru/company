using Company.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Messages
{
    public class AuthenticateCommand : IRequest
    {
        public AuthenticateCommand(Credentials credentials)
        {
            Credentials = credentials;
        }

        public Credentials Credentials { get; }
    }
}
