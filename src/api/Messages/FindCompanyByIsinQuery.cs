using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Messages
{
    public class FindCompanyByIsinQuery : IRequest<Domain.Entities.Company>
    {
        public FindCompanyByIsinQuery(string isin)
        {
            Isin = isin;
        }

        public string Isin { get; }
    }
}
