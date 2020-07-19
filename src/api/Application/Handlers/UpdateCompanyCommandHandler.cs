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
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly IRepository _repository;

        public UpdateCompanyCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            if(await _repository.AnyAsync<Domain.Entities.Company>(c => c.Isin == request.Company.Isin && c.Id != request.Company.Id))
            {
                throw new IsinExistsException();
            }

            await _repository.MergeAsync(request.Company);
            await _repository.SyncAsync();
            return Unit.Value;
        }
    }
}
