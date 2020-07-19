using Company.Api.Controllers;
using Company.Domain.Exceptions;
using Company.Domain.Types;
using Company.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Api.Controllers
{
    public class CompaniesControllerTests
    {
        [Fact]
        public async Task ShouldSendQueryRequestOnSearchCompanies()
        {
            var mediaStub = Substitute.For<IMediator>();
            var controller = new CompaniesController(mediaStub);

            await controller.SearchCompanies(new SearchCompaniesFilter());

            await mediaStub.Received(1).Send(Arg.Any<SearchCompaniesQuery>());
        }

        [Fact]
        public async Task ShouldSendFindCompanyRequestOnFindCompanyById()
        {
            var mediaStub = Substitute.For<IMediator>();
            var controller = new CompaniesController(mediaStub);

            await controller.FindCompanyById(0);

            await mediaStub.Received(1).Send(Arg.Any<FindCompanyByIdQuery>());
        }

        [Fact]
        public async Task ShouldReturnNotFoundIfCompanyNotExistsThrownOnFindCompanyById()
        {
            var mediaStub = Substitute.For<IMediator>();
            mediaStub.Send(Arg.Any<FindCompanyByIdQuery>()).Throws<CompanyNotFoundException>();
            var controller = new CompaniesController(mediaStub);

            var result = await controller.FindCompanyById(0);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ShouldSendFindCompanyRequestOnFindCompanyByIsin()
        {
            var mediaStub = Substitute.For<IMediator>();
            var controller = new CompaniesController(mediaStub);

            await controller.FindCompanyByIsin("");

            await mediaStub.Received(1).Send(Arg.Any<FindCompanyByIsinQuery>());
        }

        [Fact]
        public async Task ShouldReturnNotFoundIfCompanyNotExistsThrownOnFindCompanyByIsin()
        {
            var mediaStub = Substitute.For<IMediator>();
            mediaStub.Send(Arg.Any<FindCompanyByIsinQuery>()).Throws<CompanyNotFoundException>();
            var controller = new CompaniesController(mediaStub);

            var result = await controller.FindCompanyByIsin("");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ShouldSendCompanyCreateCommandOnCreateCompany()
        {
            var mediaStub = Substitute.For<IMediator>();
            var controller = new CompaniesController(mediaStub);

            await controller.CreateCompany(new Domain.Entities.Company());

            await mediaStub.Received(1).Send(Arg.Any<CreateCompanyCommand>());
        }

        [Fact]
        public async Task ShouldReturnConflictIfIsisnExistsOnCreateCompany()
        {
            var mediaStub = Substitute.For<IMediator>();
            mediaStub.Send(Arg.Any<CreateCompanyCommand>()).Throws<IsinExistsException>();
            var controller = new CompaniesController(mediaStub);

            var result = await controller.CreateCompany(new Domain.Entities.Company());

            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task ShouldSetRouteIdOnBodyOnCompanyUpdate()
        {
            var mediaStub = Substitute.For<IMediator>();
            var controller = new CompaniesController(mediaStub);

            await controller.UpdateCompany(1, new Domain.Entities.Company());

            await mediaStub.Received(1).Send(Arg.Is<UpdateCompanyCommand>(c => c.Company.Id == 1));
        }

        [Fact]
        public async Task ShouldReturnConflictIfIsisnExistsOnUpdateCompany()
        {
            var mediaStub = Substitute.For<IMediator>();
            mediaStub.Send(Arg.Any<UpdateCompanyCommand>()).Throws<IsinExistsException>();
            var controller = new CompaniesController(mediaStub);

            var result = await controller.UpdateCompany(0, new Domain.Entities.Company());

            Assert.IsType<ConflictObjectResult>(result);
        }
    }
}
