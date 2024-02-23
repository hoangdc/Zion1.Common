using RestSharp;
using System.Text.Json.Serialization;

namespace Zion1.Common.Helper.Api
{

    public class ApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public List<ApiResource> ApiResources { get; set; } = new List<ApiResource>();

        public ApiResource GetApiResource(string resourceName)
        {
            ApiResources = ApiResources ?? new List<ApiResource>();
            return ApiResources.FirstOrDefault(r => r.Name == resourceName);
        }

        public RestRequest GetApiRequest(string resourceName)
        {
            var restResource = GetApiResource(resourceName);
            var restRequest = new RestRequest(restResource.Resource, restResource.Method);
            restRequest.AddHeader("Content-Type", "application/json");

            return restRequest;
        }
    }

    public class ApiResource
    {
        public string Name { get; set; } = string.Empty;

        public string Resource { get; set; } = string.Empty;

        //[JsonConverter(typeof(StringEnumConverter))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Method Method { get; set; } = Method.Get;

        public int CacheTime { get; set; } = 0;

    }

}
