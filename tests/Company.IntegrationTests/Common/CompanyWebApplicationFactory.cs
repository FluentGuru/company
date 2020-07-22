using Company.Domain.Options;
using Company.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Company.IntegrationTests
{
    public class CompanyWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                AddContext(services);
            });
        }

        private void AddContext(IServiceCollection services)
        {
            services.Remove(services.SingleOrDefault(
                                d => d.ServiceType == typeof(DbContextOptions<CompanyDbContext>)));
            services.AddDbContextPool<CompanyDbContext>(options => options.UseInMemoryDatabase("CompanyMem"));
        }
    }
}
