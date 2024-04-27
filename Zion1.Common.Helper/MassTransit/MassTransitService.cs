using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zion1.Common.Helper.MassTransit
{
    public static class MassTransitService
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            //var rabbitMqSettings = configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();

            //services.AddMassTransit(mt =>
            //{
            //    mt.UsingRabbitMq((ctx, cfg) =>
            //    {
            //        cfg.Host(rabbitMqSettings.Uri, "/", c =>
            //        {
            //            c.Username(rabbitMqSettings.UserName);
            //            c.Password(rabbitMqSettings.Password);
            //        });
            //        cfg.ConfigureEndpoints(ctx);
            //    });
            //});
            return services;
        }
    }
}
