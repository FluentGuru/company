using Company.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.IntegrationTests
{
    public class CreateCompanyTests : IntegrationTestsBase, IClassFixture<CompanyWebApplicationFactory<Startup>>
    {
        public CreateCompanyTests(CompanyWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldAcceptCompanyToCreate()
        {
            var client = await CreateAuthenticatedClient();
            var company = GetCompanyToCreate();

            var result = await client.PostAsync("/api/companies", company.ToJsonContent());

            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task ShouldConflictCreatingCompanyWithExistingIsin()
        {
            var client = await CreateAuthenticatedClient();

            var company = GetCompanyToCreate();
            var result = await client.PostAsync("/api/companies", company.ToJsonContent());
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);

            result = await client.PostAsync("/api/companies", company.ToJsonContent());
            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
        }

        [Fact]
        public async Task ShouldNotAcceptInvalidCompanies()
        {
            var client = await CreateAuthenticatedClient();

            //Null or empty name
            var company = GetCompanyToCreate();
            company.Name = null;
            var result = await client.PostAsync("/api/companies", company.ToJsonContent());
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            //Null or empty exchange
            company = GetCompanyToCreate();
            company.Exchange = null;
            result = await client.PostAsync("/api/companies", company.ToJsonContent());
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            //Invalid website url
            company = GetCompanyToCreate();
            company.Website = "invalidwebsite";
            result = await client.PostAsync("/api/companies", company.ToJsonContent());
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        private Domain.Entities.Company GetCompanyToCreate()
        {
            return new Domain.Entities.Company()
            {
                Name = "Foo Inc.",
                Exchange = "FOOBAR",
                Isin = "FO000BAR0000",
                Ticker = "FOOBAR",
                Website = "http://www.foo.bar"
            };
        }
    }
}
