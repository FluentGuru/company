using Company.Domain.Exceptions;
using Company.Domain.Services;
using Company.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Application.Handlers
{
    public class FindCompanyByIdQueryHandler : IRequestHandler<FindCompanyByIdQuery, Domain.Entities.Company>
    {
        private readonly IWareHouse _wareHouse;

        public FindCompanyByIdQueryHandler(IWareHouse wareHouse)
        {
            _wareHouse = wareHouse;
        }

        public async Task<Domain.Entities.Company> Handle(FindCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _wareHouse.FindAsync<Domain.Entities.Company>(c => c.Id == request.Id);
            if (company == null)
            {
                throw new CompanyNotFoundException();
            }

            return company;
        }
    }
}
