using Company.Api;
using Company.Domain.Types;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Company.IntegrationTests
{
    public class LoginUserTests : IntegrationTestsBase
    {
        public LoginUserTests(CompanyWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldReturnOkWhenSendingCorrectCredentials()
        {
            await CreateAuthenticatedClient();
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenSendingNotExisingUserName()
        {
            var client = Factory.CreateClient();
            var credentials = new Credentials() { UserName = "super" };

            var response = await client.PostAsync("/api/auth", credentials.ToJsonContent());

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnUnAuthorizedWhenSendingIncorrectPassword()
        {
            var client = Factory.CreateClient();
            var credentials = new Credentials() { UserName = "superadmin", Password = "wrongpassword" };

            var response = await client.PostAsync("/api/auth", credentials.ToJsonContent());

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
