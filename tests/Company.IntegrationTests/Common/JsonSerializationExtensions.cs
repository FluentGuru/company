using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Company.IntegrationTests
{
    public static class JsonSerializationExtensions
    {
        public static string ToJson(this object source) => JsonConvert.SerializeObject(source);

        public static HttpContent ToJsonContent(this object source) => new StringContent(source.ToJson(), Encoding.UTF8, "application/json");

        public async static Task<T> FromJsonAsync<T>(this HttpContent content)
        {
            var data = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
