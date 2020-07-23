using Company.Messages;
using FluentValidation;
using System;

namespace Company.Api.Validators
{
    public class CompanyValidator : AbstractValidator<Domain.Entities.Company>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Isin).NotEmpty()
                .Matches("^[A-Z]{2}([A-Z0-9]){9}[0-9]$");
            RuleFor(c => c.Ticker).NotEmpty();
            RuleFor(c => c.Exchange).NotEmpty();
            RuleFor(c => c.Website)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(c => !string.IsNullOrEmpty(c.Website));
        }
    }
}
