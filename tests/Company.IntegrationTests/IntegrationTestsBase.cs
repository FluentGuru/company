using Company.Api;
using Company.Domain.Types;
using Company.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Company.IntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        public IntegrationTestsBase(CompanyWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
        }

        protected CompanyWebApplicationFactory<Startup> Factory { get; }

        protected async Task<HttpClient> CreateAuthenticatedClient()
        {
            var client = Factory.CreateClient();
            var credentials = new Credentials() { UserName = "superadmin", Password = "P@ss123" };

            var response = await client.PostAsync("/api/auth", credentials.ToJsonContent());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var token = await response.Content.ReadAsStringAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        
    }

    public static class HttpClientExtensions
    {
        public async static Task<HttpResponseMessage> GetWithQueryAsync(this HttpClient client, string uri, object query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri + ToQueryString(query));
            return await client.SendAsync(request);
        }

        private static string ToQueryString(object obj)
        {
            var parameters = obj.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(obj));
            return "?" + HttpUtility.UrlEncode(
                string.Join(
                    "&", 
                    parameters.Where(
                        p => !string.IsNullOrEmpty(
                            p.Value?.ToString()))
                    .Select(
                        p => 
                        $"{p.Key}={p.Value}")));
        }
    }
}
