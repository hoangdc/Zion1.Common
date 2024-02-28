using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Enrichers;
using Serilog.Enrichers.Sensitive;

namespace Zion1.Common.Helper.Logger
{
    public static class LogService
    {
        public static WebApplicationBuilder AddLogService(this WebApplicationBuilder builder, IConfiguration? configuration = null)
        {
            if (configuration != null)
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    "logs/log_.txt", 
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{Properties:j}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day, 
                    rollOnFileSizeLimit: true, 
                    fileSizeLimitBytes: 100*1024*1024)
                .CreateLogger();
            }
            builder.Host.UseSerilog();
            return builder;
        }
    }
}
