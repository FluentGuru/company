using Company.Api;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.IntegrationTests
{
    public class UpdateCompanyTests : IntegrationTestsBase, IClassFixture<CompanyWebApplicationFactory<Startup>>
    {
        public UpdateCompanyTests(CompanyWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldAcceptCompanyToUpdate()
        {
            var client = await CreateAuthenticatedClient();
            var company = await FindCompanyById(client, 1);

            company.Name = "Foo .Inc";
            var response = await client.PutAsync("/api/companies/1", company.ToJsonContent());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            company = await FindCompanyById(client, 1);
            Assert.Equal("Foo .Inc", company.Name);
        }

        [Fact]
        public async Task ShouldConflictUpdateIfNewIsinExists()
        {
            var client = await CreateAuthenticatedClient();
            var company = await FindCompanyById(client, 1);
            var newCompany = await CreateDefaultCompany(client);

            company.Isin = newCompany.Isin;
            var response = await client.PutAsync("/api/companies/1", company.ToJsonContent());

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task ShouldNotAcceptInvalidCompanies()
        {
            var client = await CreateAuthenticatedClient();

            //Null or empty name
            var company = await FindCompanyById(client, 1);
            company.Name = null;
            var result = await client.PutAsync("/api/companies/" + 1, company.ToJsonContent());
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            //Null or empty exchange
            company = await FindCompanyById(client, 1);
            company.Exchange = null;
            result = await client.PutAsync("/api/companies/" + 1, company.ToJsonContent());
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

            //Invalid website url
            company = await FindCompanyById(client, 1);
            company.Website = "invalidwebsite";
            result = await client.PutAsync("/api/companies/" + 1, company.ToJsonContent());
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
