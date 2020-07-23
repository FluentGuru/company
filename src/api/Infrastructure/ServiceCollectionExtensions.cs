using Company.Domain.Options;
using Company.Domain.Services;
using Company.Infrastructure.Data;
using Company.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CompanyDbContext>(options => options
            .UseSqlServer(configuration.GetValue<string>("ConnectionString")));
            services.AddTransient<IWareHouse, EfRepository>();
            services.AddTransient<IRepository, EfRepository>();
            services.AddSingleton<IHasher, Sha1Hasher>();
            services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        }
    }
}
