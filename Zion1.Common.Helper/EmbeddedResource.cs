using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text;

namespace Zion1.Common.Helper
{
    public static class EmbeddedResource
    {
        public static string GetEmbeddedFile(string fileName)
        {
            string fileContent = string.Empty;
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly());

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
