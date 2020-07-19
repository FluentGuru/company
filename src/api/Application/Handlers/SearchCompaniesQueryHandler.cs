using Company.Domain.Services;
using Company.Messages;
using MediatR;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Application.Handlers
{
    public class SearchCompaniesQueryHandler : IRequestHandler<SearchCompaniesQuery, IEnumerable<Domain.Entities.Company>>
    {
        private readonly IWareHouse _wareHouse;

        public SearchCompaniesQueryHandler(IWareHouse wareHouse)
        {
            _wareHouse = wareHouse;
        }

        public Task<IEnumerable<Domain.Entities.Company>> Handle(SearchCompaniesQuery request, CancellationToken cancellationToken)
        {
            return _wareHouse
                .FetchAsync<Domain.Entities.Company>(query => query.Where(c =>
                c.Name.Contains(request.Filter.Name)
                && c.Ticker.Contains(request.Filter.Ticker)
                && c.Exchange.Contains(request.Filter.Exchange)));
        }
    }
}
