using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection;
using System.Text;
using Zion1.Common.Helper.Cache;

namespace Zion1.Common.Helper.Api
{
    public static class ApiHelper
    {
        private static ICacheService _cache = new InMemoryCache();

        public static ApiSettings ApiSettings
        {
            get
            {
                return _cache.Get<ApiSettings>("ApiSettings_Global");
            }
            set
            {
                _cache.Set("ApiSettings_Global", value, 24 * 60);
            }
        }

        public static async Task<IServiceCollection> AddApiSettings(this IServiceCollection services)
        {
            //Get Api Settings from ApiSettings.json 
            ApiSettings = await GetApiSettings();
            //Register HttpClient
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(ApiSettings.BaseUrl) });
            return services;
        }

        public static async Task<ApiSettings> GetApiSettings(string apiSettingConfigFile = "ApiSettings.json")
        {
            var apiSettingJson = GetEmbeddedFile(apiSettingConfigFile, Assembly.GetEntryAssembly());
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

        public static string GetEmbeddedFile(string fileName, Assembly assembly)
        {
            string fileContent = string.Empty;
            var embeddedProvider = new EmbeddedFileProvider(assembly);

            using (var reader = embeddedProvider.GetFileInfo(fileName).CreateReadStream())
            {
                byte[] bytes = new byte[reader.Length];
                reader.Read(bytes);

                fileContent = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            }

            return fileContent;
        }
    }
}
