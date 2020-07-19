using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Company.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
