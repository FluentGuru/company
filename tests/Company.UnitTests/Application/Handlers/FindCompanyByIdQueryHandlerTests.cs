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
    public class FindCompanyByIdQueryHandlerTests
    {
        [Fact]
        public async Task ShouldReturnCorrectCompany()
        {
            var company1 = new Domain.Entities.Company() { Id = 1 };
            var company2 = new Domain.Entities.Company { Id = 2 };
            var repo = new MockRepository();
            await repo.CommitAsync(company1, company2);

            var result = await new FindCompanyByIdQueryHandler(repo).Handle(new FindCompanyByIdQuery(2), default);

            Assert.Equal(company2, result);
        }
    }
}
