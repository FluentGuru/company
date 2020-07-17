using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Domain.Entities;
using Company.Domain.Exceptions;
using Company.Domain.Types;
using Company.Messages;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        [Produces(typeof(IEnumerable<Domain.Entities.Company>))]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Company>>> SearchCompanies([FromQuery]SearchCompaniesFilter filter)
        {
            var result = await _mediator.Send(new SearchCompaniesQuery(filter));
            return new ActionResult<IEnumerable<Domain.Entities.Company>>(result);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces(typeof(Domain.Entities.Company))]
        public async Task<ActionResult<Domain.Entities.Company>> FindCompanyById([FromRoute]int id)
        {
            try
            {
                var result = await _mediator.Send(new FindCompanyByIdQuery(id));
                return new ActionResult<Domain.Entities.Company>(result);
            }
            catch(CompanyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("isbn/{isbn}")]
        [Produces(typeof(Domain.Entities.Company))]
        public async Task<ActionResult<Domain.Entities.Company>> FindCompanyByIsbn([FromRoute] string isbn)
        {
            try
            {
                var result = await _mediator.Send(new FindCompanyByIsbnQuery(isbn));
                return new ActionResult<Domain.Entities.Company>(result);
            }
            catch (CompanyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> CreateCompany([FromBody]Domain.Entities.Company company)
        {
            try
            {
                await _mediator.Send(new CreateCompanyCommand(company));
                return Accepted();
            }
            catch(IsbnExistsException)
            {
                return IsbnConflict(company);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateCompany([FromRoute]int id, [FromBody]Domain.Entities.Company company)
        {
            try
            {
                company.Id = id;
                await _mediator.Send(new UpdateCompanyCommand(company));
                return Ok();
            }
            catch(IsbnExistsException)
            {
                return IsbnConflict(company);
            }
        }

        private ActionResult IsbnConflict(Domain.Entities.Company company)
        {
            return Conflict($"Another company is already registered with the ISBN '{company.Isbn}'");
        }
    }
}
