using Company.Domain.Entities;
using Company.Domain.Exceptions;
using Company.Domain.Options;
using Company.Domain.Services;
using Company.Messages;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Application.Handlers
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand>
    {
        private readonly AuthOptions _options;

        public AuthenticateCommandHandler(IOptions<AuthOptions> options)
        {
            _options = options.Value;
        }

        public Task<Unit> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            if(request.Credentials.UserName != _options.UserName)
            {
                throw new UserNotFoundException();
            }

            if(request.Credentials.Password != _options.Password)
            {
                throw new AuthenticationFailedException();
            }

            return Unit.Task;
        }
    }
}
