using System.Reflection;
using First.Subscribers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shared.Settings;

namespace First
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            // SHARED_LIBRARY:
            //  services.AddMassTransitWithRabbitMQ();
            services.AddMassTransit(options =>
            {
                // Allow to listen to all events
                options.AddConsumers(Assembly.GetEntryAssembly());
                // or
                // Allow to listen to certain Events
                // options.AddConsumer<PaymentRemovedSubscriber>();

                // optional only for cosmetic change
                // options.SetKebabCaseEndpointNameFormatter();
                
                options.UsingRabbitMq((context, configuration) =>
                {
                    configuration.Host(Configuration["EventBusSettings:HostAddress"]);

                    // allow to configure endpoints for registered events automatically
                    configuration.ConfigureEndpoints(context);
                    // or
                    // configure subscriber's names to segregaters events to certain
                    // name instead of using one endpoint name for event

                    // configuration.ReceiveEndpoint("payment-queue", configure => 
                    // {
                    //     configure.ConfigureConsumer<PaymentAddedSubscriber>(context);
                    //     configure.ConfigureConsumer<PaymentUpdatedConsumer>(context);
                    // });
                });
            });
            services.AddMassTransitHostedService();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "First", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "First v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
