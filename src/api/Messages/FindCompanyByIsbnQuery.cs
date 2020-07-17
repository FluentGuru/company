using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Messages
{
    public class FindCompanyByIsbnQuery : IRequest<Domain.Entities.Company>
    {
        public FindCompanyByIsbnQuery(string isbn)
        {
            Isbn = isbn;
        }

        public string Isbn { get; }
    }
}
