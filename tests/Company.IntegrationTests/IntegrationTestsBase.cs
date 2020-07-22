using Company.Api;
using Company.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.IntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        public IntegrationTestsBase(CompanyWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
        }

        protected CompanyWebApplicationFactory<Startup> Factory { get; }

        protected async Task AddCompanies(params Domain.Entities.Company[] companies)
        {
            var context = Factory.Services.GetRequiredService<CompanyDbContext>();
            context.AddRange(companies);
            await context.SaveChangesAsync();
        }

        protected Task AddSampleCompanies()
            => AddCompanies(
                GetCompany("Apple Inc.", "NASDAQ", "AAPL", "US0378331005", "http://www.apple.com"),
                GetCompany("British Airways", "Plc Pink Sheets", "BAIRY", "US1104193065"),
                GetCompany("Heineken NV", "Euronext Amsterdam", "HEIA", "NL0000009165"),
                GetCompany("Panasonic Corp", "Tokyo Stock Exchange", "6752", "JP3866800000", "http://www.panasonic.co.jp"),
                GetCompany("Porsche Automobil", "Deutsche Börse", "PAH3", "DE000PAH0038", "https://www.porsche.com/")
                );

        protected Domain.Entities.Company GetCompany(string name = "", string isin = "", string exchange = "", string ticker = "", string website = "")
            => new Domain.Entities.Company()
            {
                Name = name,
                Isin = isin,
                Exchange = exchange,
                Ticker = ticker,
                Website = website
            };
    }
}
