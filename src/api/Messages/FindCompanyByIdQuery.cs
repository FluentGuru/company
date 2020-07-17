using Company.Domain.Entities;
using MediatR;
using System;

namespace Company.Messages
{
    public class FindCompanyByIdQuery : IRequest<Domain.Entities.Company>
    {
        public FindCompanyByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
