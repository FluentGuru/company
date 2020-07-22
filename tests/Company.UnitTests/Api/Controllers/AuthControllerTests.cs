using Company.Api.Controllers;
using Company.Domain.Exceptions;
using Company.Domain.Options;
using Company.Domain.Types;
using Company.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Api.Controllers
{
    public class AuthControllerTests
    {
        

        [Fact]
        public async Task ShouldReturnTokenOnSuccessfulLogIn()
        {
            var mediaStub = Substitute.For<IMediator>();
            IOptions<JwtOptions> jwtStub = GetJwtOptions();
            Credentials credentials = GetTestCredentials();

            var result = await new AuthController(mediaStub, jwtStub).Login(credentials);

            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public async Task ShouldReturnNotFoundIfUserNotExists()
        {
            var mediaStub = Substitute.For<IMediator>();
            mediaStub.Send(Arg.Any<AuthenticateCommand>()).Returns<Unit>(x => throw new UserNotFoundException());
            IOptions<JwtOptions> jwtStub = GetJwtOptions();
            Credentials credentials = GetTestCredentials();

            var result = await new AuthController(mediaStub, jwtStub).Login(credentials);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ShouldReturnUnauthorizedIfAuthenticationFails()
        {
            var mediaStub = Substitute.For<IMediator>();
            mediaStub.Send(Arg.Any<AuthenticateCommand>()).Returns<Unit>(x => throw new AuthenticationFailedException());
            IOptions<JwtOptions> jwtStub = GetJwtOptions();
            Credentials credentials = GetTestCredentials();

            var result = await new AuthController(mediaStub, jwtStub).Login(credentials);

            Assert.IsType<UnauthorizedResult>(result.Result);
        }

        private static Credentials GetTestCredentials()
        {
            return new Credentials() { UserName = "johndoe" };
        }

        private static IOptions<JwtOptions> GetJwtOptions()
        {
            var jwtStub = Substitute.For<IOptions<JwtOptions>>();
            jwtStub.Value.Returns(new JwtOptions() { Issuer = "Test", Key = "ThisIsMySecretKey" });
            return jwtStub;
        }
    }
}
