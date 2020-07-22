using Company.Api;
using Company.Domain.Types;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Company.IntegrationTests
{
    public class LoginUserTests : IClassFixture<CompanyWebApplicationFactory<Startup>>
    {
        private readonly CompanyWebApplicationFactory<Startup> _factory;

        public LoginUserTests(CompanyWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnOkWhenSendingCorrectCredentials()
        {
            var client = _factory.CreateClient();
            var credentials = new Credentials() { UserName = "superadmin", Password = "P@ss123" };

            var response = await client.PostAsync("/api/auth", credentials.ToJsonContent());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenSendingNotExisingUserName()
        {
            var client = _factory.CreateClient();
            var credentials = new Credentials() { UserName = "super" };

            var response = await client.PostAsync("/api/auth", credentials.ToJsonContent());

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnUnAuthorizedWhenSendingIncorrectPassword()
        {
            var client = _factory.CreateClient();
            var credentials = new Credentials() { UserName = "superadmin", Password = "wrongpassword" };

            var response = await client.PostAsync("/api/auth", credentials.ToJsonContent());

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
