using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using Zion1.Common.Helper;

namespace Zion1.Common.API.Consumer
{
    
    public class ApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public List<ApiResource> ApiResources { get; set; } = new List<ApiResource>();

        public ApiResource GetApiResource(string resourceName)
        {
            return ApiResources.FirstOrDefault(r => r.Name == resourceName);
        }
    }

    public class ApiResource
    {
        public string Name { get; set; } = string.Empty;

        public string Resource { get; set; } = string.Empty;

        [JsonConverter(typeof(StringEnumConverter))]
        public Method Method { get; set; } = Method.Get;

        public int CacheTime { get; set; } = 0;

    }

}
