using Company.Domain.Options;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Company.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
            services.Configure<AuthOptions>(option => configuration.GetSection("Auth").Bind(option));
        }
    }
}
