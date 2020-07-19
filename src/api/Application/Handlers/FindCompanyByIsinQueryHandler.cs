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
    public class FindCompanyByIsinQueryHandler : IRequestHandler<FindCompanyByIsinQuery, Domain.Entities.Company>
    {
        private readonly IWareHouse _wareHouse;

        public FindCompanyByIsinQueryHandler(IWareHouse wareHouse)
        {
            _wareHouse = wareHouse;
        }

        public async Task<Domain.Entities.Company> Handle(FindCompanyByIsinQuery request, CancellationToken cancellationToken)
        {
            var company = await _wareHouse.FindAsync<Domain.Entities.Company>(c => c.Isin == request.Isin);
            if(company == null)
            {
                throw new CompanyNotFoundException();
            }

            return company;
        }
    }
}
