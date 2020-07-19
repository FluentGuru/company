using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Company.Application.Handlers;
using Company.Domain.Entities;
using Company.Domain.Exceptions;
using Company.Domain.Services;
using Company.Messages;
using Company.UnitTests.Commons;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Application.Handlers
{
    public class CreateCompanyCommandHandlerTests
    {
        [Fact]
        public async Task ShouldSaveNewCompany()
        {
            var repoStub = Substitute.For<IRepository>();

            await new CreateCompanyCommandHandler(repoStub).Handle(new CreateCompanyCommand(new Domain.Entities.Company()), default);

            await repoStub.Received(1).CommitAsync(Arg.Any<IEnumerable<Domain.Entities.Company>>());
            await repoStub.Received(1).SyncAsync();
        }

        [Fact]
        public async Task ShouldThrowExistingIsinIfExists()
        {
            var repo = new MockRepository();
            await repo.CommitAsync(new Domain.Entities.Company() { Isin = "TEST" });

            await Assert.ThrowsAsync<IsinExistsException>(
                () => new CreateCompanyCommandHandler(repo).Handle(
                    new CreateCompanyCommand(
                        new Domain.Entities.Company() 
                        { 
                            Isin = "TEST" 
                        }), default));
        }
    }
}
