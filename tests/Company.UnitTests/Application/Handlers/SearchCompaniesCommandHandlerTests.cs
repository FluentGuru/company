using Company.Application.Handlers;
using Company.Domain.Services;
using Company.Domain.Types;
using Company.Messages;
using Company.UnitTests.Commons;
using NSubstitute.Routing.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Application.Handlers
{
    public class SearchCompaniesCommandHandlerTests
    {
        [Fact]
        public async Task ShouldSearchCompaniesEqualsTheName()
        {
            var handler = await GetSearchHandler();

            var result = await handler.Handle(GetQuery(name: "Apple .Inc"), default);

            Assert.Single(result, c => c.Name == "Apple .Inc");
            Assert.Equal("Apple .Inc", result.First().Name);
        }

        [Fact]
        public async Task ShouldSearchCompaniesLikeTheName()
        {
            var handler = await GetSearchHandler();

            var result = await handler.Handle(GetQuery(name: "Apple"), default);

            Assert.Single(result, c => c.Name == "Apple .Inc");
            Assert.Equal("Apple .Inc", result.First().Name);
        }

        private Domain.Entities.Company GetCompanyToSearch(string name, string exchange, string ticker)
            => new Domain.Entities.Company()
            {
                Name = name,
                Exchange = exchange,
                Ticker = ticker
            };

        private Domain.Entities.Company[] GetCompaniesToSearch()
            => new[]
            {
                GetCompanyToSearch("Apple .Inc", "NASDAQ", "APPL"),
                GetCompanyToSearch("British Airways Plc", "Pink Sheets", "BAIRY"),
                GetCompanyToSearch("Heineken NV", "Euronext Amsterdam", "HEIA"),
                GetCompanyToSearch("Panasonic Corp", "Tokyo Stock Exchange", "6752"),
                GetCompanyToSearch("Porsche Automobil", "Deutsche Börse", "PAH3"),
            };

        private async Task<IRepository> GetRepoToSearch()
        {
            var repo = new MockRepository();
            await repo.CommitAsync(GetCompaniesToSearch());
            return repo;
        }

        private async Task<SearchCompaniesQueryHandler> GetSearchHandler()
            => new SearchCompaniesQueryHandler(await GetRepoToSearch());

        private SearchCompaniesQuery GetQuery(string name = "", string exchange = "", string ticker = "")
            => new SearchCompaniesQuery(new SearchCompaniesFilter()
            {
                Name = name,
                Exchange = exchange,
                Ticker = ticker
            });
    }
}
