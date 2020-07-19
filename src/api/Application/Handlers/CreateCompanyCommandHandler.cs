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
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand>
    {
        private readonly IRepository _repository;

        public CreateCompanyCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if(await _repository.AnyAsync<Domain.Entities.Company>(c => c.Isin == request.Company.Isin))
            {
                throw new IsinExistsException();
            }

            await _repository.CommitAsync(request.Company);
            await _repository.SyncAsync();

            return Unit.Value;
        }
    }
}
