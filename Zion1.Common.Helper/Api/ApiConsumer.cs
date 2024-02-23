using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Zion1.Common.Helper.Api
{
    public class ApiConsumer : RestClient
    {
        public ApiSettings ApiSettings { get; set; } = new ApiSettings();
        public RestClient ApiClient { get; set; } = new RestClient();

        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();
        public object? Body { get; set; }
        

        /// <summary>
        /// Construct Api Consumer
        /// </summary>
        /// <param name="apiSettings">This is api settings from json config</param>
        public ApiConsumer(ApiSettings apiSettings)
        {
            //Get Api Settings from config file
            ApiSettings = apiSettings;
            
        }

        public RestRequest GetApiRequest(string resourceName)
        {
            var restResource = ApiSettings.GetApiResource(resourceName);
            var restRequest = new RestRequest(restResource.Resource, restResource.Method);
            restRequest.AddHeader("Content-Type", "application/json");

            return restRequest;
        }

        public async Task<RestResponse> ExecuteAsync(string resourceName)
        {
            var apiRequest = GetApiRequest(resourceName);

            if (Params.Count > 0)
            {
                foreach (var param in Params)
                {
                    apiRequest.AddUrlSegment(param.Key, param.Value);
                }
                Params.Clear();
            }

            if (Body != null)
            {
                apiRequest.AddBody(JsonConvert.SerializeObject(Body), "application/json");
            }

            //Init ApiClient
            ApiClient = new RestClient(ApiSettings.BaseUrl);

            return await ApiClient.ExecuteAsync(apiRequest);
        }

        /// <summary>
        /// This method uses for SSR, CSR and Auto
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public async Task<TResponse?> ExecuteJsonAsync<TResponse>(string resourceName)
        {
            var restResource = ApiSettings.GetApiResource(resourceName);
            switch (restResource.Method)
            {
                case Method.Get:
                    if (Params.Count > 0)
                    {
                        return await this.GetJsonAsync<TResponse>(ApiSettings.BaseUrl + restResource.Resource, Params);
                    }
                    return await this.GetJsonAsync<TResponse>(ApiSettings.BaseUrl + restResource.Resource);
                case Method.Post:
                    return await this.PostJsonAsync<object, TResponse>(ApiSettings.BaseUrl + restResource.Resource, Body);
                case Method.Put:
                    return await this.PutJsonAsync<object, TResponse>(ApiSettings.BaseUrl + restResource.Resource, Body);
                case Method.Delete:
                default:
                    return default(TResponse);
            }

        }
    }
}
