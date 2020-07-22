using Company.Api;
using Company.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.IntegrationTests
{
    public class FindCompanyByIdTests : IntegrationTestsBase, IClassFixture<CompanyWebApplicationFactory<Startup>>
    {
        public FindCompanyByIdTests(CompanyWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldReturnCompanyById()
        {
            await AddSampleCompanies();
            var client = Factory.CreateClient();

            var response = await client.GetAsync("/api/companies/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var company = await response.Content.FromJsonAsync<Domain.Entities.Company>();
            Assert.Equal("Apple .Inc", company.Name);
        }
    }
}
