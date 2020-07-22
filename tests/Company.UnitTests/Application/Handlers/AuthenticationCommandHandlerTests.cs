using Company.Application.Handlers;
using Company.Domain.Exceptions;
using Company.Domain.Options;
using Company.Messages;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Application.Handlers
{
    public class AuthenticationCommandHandlerTests
    {
        private AuthOptions GetUser(string username = "johndoe", string password = "pass@123")
            => new AuthOptions()
            {
                UserName = username,
                Password = password
            };

        private IOptions<AuthOptions> GetAuthOptions()
        {
            var options = Substitute.For<IOptions<AuthOptions>>();
            options.Value.Returns(GetUser());
            return options;
        }

        private AuthenticateCommandHandler GetHandler()
            => new AuthenticateCommandHandler(GetAuthOptions());

        private AuthenticateCommand GetAuthCommand(string username = "johndoe", string password = "pass@123")
            => new AuthenticateCommand(GetUser(username, password));

        [Fact]
        public async Task ShouldThrowNotFoundIfUserNameDifferent()
        {
            var handler = GetHandler();

            await Assert.ThrowsAsync<UserNotFoundException>(() => handler.Handle(GetAuthCommand("john"), default));
        }

        [Fact]
        public async Task ShouldThrowAuthenticationFailedIfPasswordDifferent()
        {
            var handler = GetHandler();

            await Assert.ThrowsAsync<AuthenticationFailedException>(() => handler.Handle(GetAuthCommand(password: "pa"), default));
        }
    }
}
