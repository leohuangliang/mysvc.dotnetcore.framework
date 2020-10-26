using System;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Product.Application.IntegrationEvents.EventHandling;

namespace Sample.Product.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class MassTransitExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers(typeof(OrderCreatedIntegrationEventHandler));
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://127.0.0.1:5672/frameworksample"), h =>
                    {
                        h.Username("admin");
                        h.Password("admin123456");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
