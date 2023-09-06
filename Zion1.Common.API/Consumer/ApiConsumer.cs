using Newtonsoft.Json;
using RestSharp;
using Zion1.Common.Helper;

namespace Zion1.Common.API.Consumer
{
    public class ApiComsumer
    {
        private ApiSettings _apiSettings = new ApiSettings();
        private ApiResource _apiResource = new ApiResource();

        public RestClient ApiClient { get; set; } = new RestClient();
        public RestRequest ApiRequest { get; set; } = new RestRequest();

        public string ResourceName 
        { 
            set 
            {
                //Get specific Api Resource 
                _apiResource = _apiSettings.GetApiResource(value);
                ApiRequest = new RestRequest(_apiResource.Resource, _apiResource.Method);
            } 
        }

        public ApiComsumer()
        {
            //Get Api Settings from config file
            _apiSettings = GetApiSettings(); 
            //Init Apiclient and ApiRequest with Api Resource above
            ApiClient = new RestClient(_apiSettings.BaseUrl);
        }

        public ApiComsumer(string resourceName) : this()
        {
            ResourceName = resourceName;
        }

        private ApiSettings GetApiSettings(string apiSettingConfigFile = "ApiSettings.json")
        {
            var apiSettingJson = EmbeddedResource.GetEmbeddedFile(apiSettingConfigFile);
            return JsonConvert.DeserializeObject<ApiSettings>(apiSettingJson);
        }

        public async Task<List<T>> ExecuteAsync<T>(string resourceName = "")
        {
            if(!string.IsNullOrEmpty(resourceName))
                ResourceName = resourceName;
            var response = await ApiClient.ExecuteAsync<T>(ApiRequest);

            return JsonConvert.DeserializeObject<List<T>>(response.Content);
        }
    }
}
