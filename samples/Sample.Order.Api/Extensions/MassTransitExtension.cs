﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Order.Application.DomainEventHandlers;

namespace Sample.Order.Api.Extensions
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