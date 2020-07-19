using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Infrastructure.Data.Configurations
{
    public class CompanyConfigurations : IEntityTypeConfiguration<Domain.Entities.Company>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Company> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.Isin).IsRequired().HasMaxLength(12);
            builder.HasIndex(m => m.Isin).IsUnique();

            builder.Property(m => m.Name).IsRequired();

            builder.Property(m => m.Exchange).IsRequired();

            builder.Property(m => m.Ticker).IsRequired();

            builder.Property(m => m.Website).HasMaxLength(253);
        }
    }
}
