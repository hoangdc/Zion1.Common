using Newtonsoft.Json;
using RestSharp;

namespace Zion1.Common.Helper.Api
{
    public class ApiConsumer
    {
        private ApiResource _apiResource = new ApiResource();

        public ApiSettings ApiSettings { get; set; } = new ApiSettings();
        public RestClient ApiClient { get; set; } = new RestClient();
        public RestRequest ApiRequest { get; set; } = new RestRequest();

        public string ResourceName 
        { 
            set 
            {
                //Get specific Api Resource 
                _apiResource = ApiSettings.GetApiResource(value);
                ApiRequest = new RestRequest(_apiResource.Resource, _apiResource.Method);
                ApiRequest.AddHeader("Content-Type", "application/json");
            } 
        }

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
            //Init ApiClient and ApiRequest with Api Resource above
            ApiClient = new RestClient(ApiSettings.BaseUrl);

        }

        public async Task<RestResponse> ExecuteAsync(string resourceName)
        {
            if(!string.IsNullOrEmpty(resourceName))
                ResourceName = resourceName;

            if(Params.Count > 0)
            {
                foreach (var param in Params)
                {
                    ApiRequest.AddUrlSegment(param.Key, param.Value);
                }
                Params.Clear();
            }

            if(Body != null)
            {
                ApiRequest.AddBody(JsonConvert.SerializeObject(Body), "application/json");
            }

            var response = await ApiClient.ExecuteAsync(ApiRequest);
            return response;
        }
    }
}
