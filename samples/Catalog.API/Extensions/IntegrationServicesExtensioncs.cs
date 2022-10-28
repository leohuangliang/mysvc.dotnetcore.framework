using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.Framework.Infrastructure.Crosscutting.Json;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;

using MySvc.Framework.Infrastructure.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Catalog.API.Extensions
{
    public static class IntegrationServicesExtensioncs
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJsonConverter, NewtonsoftJsonConverter>();
           
            return services;
        }
    }
}
