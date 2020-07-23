using Company.Api.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Company.UnitTests.Api.Validators
{
    public class CompanyValidatorTests
    {
        [Fact]
        public void ShouldPassValidation()
        {
            var validator = new CompanyValidator();
            var company = GetCompanyToValidate();

            var result = validator.Validate(company);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldFailValidationIfNameNullOrEmptyOrWhiteSpace()
        {
            var validator = new CompanyValidator();
            var company = GetCompanyToValidate();

            company.Name = null;
            var result = validator.Validate(company);
            Assert.False(result.IsValid);

            company.Name = "";
            result = validator.Validate(company);
            Assert.False(result.IsValid);

            company.Name = " ";
            result = validator.Validate(company);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldFailValidationIfIsinNullOrEmptyOrWhiteSpace()
        {
            var validator = new CompanyValidator();
            var company = GetCompanyToValidate();

            company.Isin = null;
            var result = validator.Validate(company);
            Assert.False(result.IsValid);

            company.Isin = "";
            result = validator.Validate(company);
            Assert.False(result.IsValid);

            company.Isin = " ";
            result = validator.Validate(company);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldFailValidationIfInvalidIsin()
        {
            var validator = new CompanyValidator();
            var company = GetCompanyToValidate();

            company.Isin = "invalid";
            var result = validator.Validate(company);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldFailValidationIfExchangeNullOrEmptyOrWhiteSpace()
        {
            var validator = new CompanyValidator();
            var company = GetCompanyToValidate();

            company.Exchange = null;
            var result = validator.Validate(company);
            Assert.False(result.IsValid);

            company.Exchange = "";
            result = validator.Validate(company);
            Assert.False(result.IsValid);

            company.Exchange = " ";
            result = validator.Validate(company);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldFailValidationIfWebsiteIsNotValidUrl()
        {
            var validator = new CompanyValidator();
            var company = GetCompanyToValidate();

            company.Website = "invalid";
            var result = validator.Validate(company);

            Assert.False(result.IsValid);
        }

        private Domain.Entities.Company GetCompanyToValidate()
            => GetCompany("Apple Inc.", "NASDAQ", "AAPL", "US0378331005", "http://www.apple.com");

        private Domain.Entities.Company GetCompany(string name = "", string exchange = "", string ticker = "", string isin = "", string website = "")
            => new Domain.Entities.Company()
            {
                Name = name,
                Isin = isin,
                Exchange = exchange,
                Ticker = ticker,
                Website = website
            };
    }
}
