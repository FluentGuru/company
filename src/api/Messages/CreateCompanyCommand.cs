using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Messages
{
    public class CreateCompanyCommand : IRequest
    {
        public CreateCompanyCommand(Domain.Entities.Company company)
        {
            Company = company;
        }

        public Domain.Entities.Company Company { get; }
    }
}
