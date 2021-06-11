using MassTransit;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using MassTransit.Definitions;

namespace Shared.Settings
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumers(Assembly.GetEntryAssembly());
                options.UsingRabbitMQ((context, configuration) =>
                {
                    var config = context.GetService<IConfiguration>();
                    configuration.Host(config["EventBusSettings:HostAddress"]);
                    //configuration.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("one", false));
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}