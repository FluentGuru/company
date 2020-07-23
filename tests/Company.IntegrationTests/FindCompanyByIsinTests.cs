using Company.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.IntegrationTests
{
    public class FindCompanyByIsinTests : IntegrationTestsBase, IClassFixture<CompanyWebApplicationFactory<Startup>>
    {
        public FindCompanyByIsinTests(CompanyWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldFindCompanyByIsin()
        {
            var client = await CreateAuthenticatedClient();

            var response = await client.GetAsync("/api/companies/isin/US0378331005");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var company = await response.Content.FromJsonAsync<Domain.Entities.Company>();
            Assert.Equal("Apple Inc.", company.Name);
        }

        [Fact]
        public async Task ShouldReturnNotFoundIfIsinNotExists()
        {
            var client = await CreateAuthenticatedClient();

            var response = await client.GetAsync("/api/companies/isin/NOTFOUND0000000");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
