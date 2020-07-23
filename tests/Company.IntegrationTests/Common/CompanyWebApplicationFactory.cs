using Company.Domain.Options;
using Company.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Company.IntegrationTests
{
    public class CompanyWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                AddContext(services);
                
                using(var scope = services.BuildServiceProvider().CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
                    AddSampleCompanies(context).GetAwaiter().GetResult();
                }
            });
        }

        private void AddContext(IServiceCollection services)
        {
            services.Remove(services.SingleOrDefault(
                                d => d.ServiceType == typeof(DbContextOptions<CompanyDbContext>)));
            services.AddDbContext<CompanyDbContext>(options => options.UseInMemoryDatabase("CompanyMem" + this.GetHashCode()));
        }

        protected async Task AddCompanies(CompanyDbContext context, params Domain.Entities.Company[] companies)
        {
            context.AddRange(companies);
            await context.SaveChangesAsync();
        }

        protected Task AddSampleCompanies(CompanyDbContext context)
            => AddCompanies(
                context,
                GetCompany("Apple Inc.", "NASDAQ", "AAPL", "US0378331005", "http://www.apple.com"),
                GetCompany("British Airways", "Plc Pink Sheets", "BAIRY", "US1104193065"),
                GetCompany("Heineken NV", "Euronext Amsterdam", "HEIA", "NL0000009165"),
                GetCompany("Panasonic Corp", "Tokyo Stock Exchange", "6752", "JP3866800000", "http://www.panasonic.co.jp"),
                GetCompany("Porsche Automobil", "Deutsche Börse", "PAH3", "DE000PAH0038", "https://www.porsche.com/")
                );

        protected Domain.Entities.Company GetCompany(string name = "", string exchange = "", string ticker = "", string isin = "", string website = "")
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
