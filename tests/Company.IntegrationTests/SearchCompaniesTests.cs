using Company.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.IntegrationTests
{
    public class SearchCompaniesTests : IntegrationTestsBase, IClassFixture<CompanyWebApplicationFactory<Startup>>
    {
        public SearchCompaniesTests(CompanyWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldReturnAllCompaniesIfNoQuery()
        {
            var client = await CreateAuthenticatedClient();

            var response = await client.GetAsync("/api/companies");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var companies = await response.Content.FromJsonAsync<IEnumerable<Domain.Entities.Company>>();
            Assert.Equal(5, companies.Count());
        }

        [Fact]
        public async Task ShouldSearchCompaniesByExactName()
        {
            var client = await CreateAuthenticatedClient();

            var response = await client.GetWithQueryAsync("/api/companies", new { name = "Apple Inc." });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var companies = await response.Content.FromJsonAsync<IEnumerable<Domain.Entities.Company>>();
            Assert.Single(companies, c => c.Name == "Apple Inc.");
        }
    }
}
