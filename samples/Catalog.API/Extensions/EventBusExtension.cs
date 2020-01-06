﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Cap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Extensions
{
    public static class EventBusExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEventBus, CapEventBus>();
            //services.AddTransient<OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
            //services.AddTransient<OrderStatusChangedToPaidIntegrationEventHandler>();

            return services;
        }
    }
}
