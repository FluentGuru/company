using Company.Application.Handlers;
using Company.Domain.Exceptions;
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
    public class UpdateCompanyCommandHandlerTests
    {
        [Fact]
        public async Task ShouldThrowIsinExistsExceptionIfAnotherCompanyHasIsin()
        {
            var company1 = new Domain.Entities.Company() { Id = 1, Isin = "test1" };
            var company2 = new Domain.Entities.Company { Id = 2, Isin = "test2" };
            var repo = new MockRepository();
            await repo.CommitAsync(company1, company2);

            await Assert.ThrowsAsync<IsinExistsException>(() => GetHandler(repo).Handle(
                new UpdateCompanyCommand(new Domain.Entities.Company()
                {
                    Id = 2,
                    Isin = "test1"
                }), default));
        }

        [Fact]
        public async Task ShouldUpdateCompanyName()
        {
            var company1 = new Domain.Entities.Company() { Id = 1, Isin = "test1" };
            var company2 = new Domain.Entities.Company { Id = 2, Isin = "test2", Name = "John .Inc" };
            var repo = new MockRepository();
            await repo.CommitAsync(company1, company2);

            await GetHandler(repo).Handle(new UpdateCompanyCommand(new Domain.Entities.Company()
            {
                Id = 2,
                Name = "Jane .Inc"
            }), default);

            var actual = await repo.FindAsync<Domain.Entities.Company>(c => c.Id == 2);
            Assert.Equal("Jane .Inc", actual.Name);
        }

        private static UpdateCompanyCommandHandler GetHandler(MockRepository repo)
        {
            return new UpdateCompanyCommandHandler(repo);
        }
    }
}
