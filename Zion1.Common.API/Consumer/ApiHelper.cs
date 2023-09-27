using Newtonsoft.Json;
using RestSharp;
using System.Reflection;
using Zion1.Common.Helper;

namespace Zion1.Common.API.Consumer
{
    public static class ApiHelper
    {
        public static ApiSettings GetApiSettings(string apiSettingConfigFile = "ApiSettings.json")
        {
            var apiSettingJson = EmbeddedResource.GetEmbeddedFile(apiSettingConfigFile, Assembly.GetCallingAssembly());
            return Convert<ApiSettings>(apiSettingJson);
        }

        public static T Convert<T>(this string jSon)
        {
            return JsonConvert.DeserializeObject<T>(jSon);
        }

        public static T Convert<T>(this RestResponse restResponse)
        {
            return Convert<T>(restResponse.Content);
        }
    }
}
