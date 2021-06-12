using MassTransit;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit.Definition;

namespace Shared.Settings
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumers(Assembly.GetEntryAssembly());
                options.SetKebabCaseEndpointNameFormatter();
                options.UsingRabbitMq((context, configuration) =>
                {
                    var config = context.GetService<IConfiguration>();
                    configuration.Host(config["EventBusSettings:HostAddress"]);
                    configuration.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}