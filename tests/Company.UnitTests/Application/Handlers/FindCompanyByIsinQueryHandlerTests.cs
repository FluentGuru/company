using Company.Application.Handlers;
using Company.Domain.Services;
using Company.Messages;
using Company.UnitTests.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Application.Handlers
{
    public class FindCompanyByIsinQueryHandlerTests
    {
        [Fact]
        public async Task ShouldFindCorrectCompanyByIsin()
        {
            var company1 = new Domain.Entities.Company() { Isin = "test1" };
            var company2 = new Domain.Entities.Company { Isin = "test2" };
            var repo = new MockRepository();
            await repo.CommitAsync(company1, company2);

            var result = await new FindCompanyByIsinQueryHandler(repo).Handle(new FindCompanyByIsinQuery("test1"), default);

            Assert.Equal(company1, result);
        }
    }
}
