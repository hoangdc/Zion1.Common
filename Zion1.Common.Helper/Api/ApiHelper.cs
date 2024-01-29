using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection;
using System.Text;

namespace Zion1.Common.Helper.Api
{
    public static class ApiHelper
    {
        public static ApiSettings GetApiSettings(string apiSettingConfigFile = "ApiSettings.json")
        {
            var apiSettingJson = GetEmbeddedFile(apiSettingConfigFile, Assembly.GetCallingAssembly());
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
