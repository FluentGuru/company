using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Company.IntegrationTests
{
    public static class JsonSerializationExtensions
    {
        public static string ToJson(this object source) => JsonConvert.SerializeObject(source);

        public static HttpContent ToJsonContent(this object source) => new StringContent(source.ToJson(), Encoding.UTF8, "application/json");
    }
}
