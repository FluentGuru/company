using Company.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Messages
{
    public class SearchCompaniesQuery : IRequest<IEnumerable<Domain.Entities.Company>>
    {
        public SearchCompaniesQuery(SearchCompaniesFilter filter)
        {
            Filter = filter;
        }

        public SearchCompaniesFilter Filter { get; }
    }
}
