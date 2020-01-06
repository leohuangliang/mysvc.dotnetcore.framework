using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;

using MySvc.DotNetCore.Framework.Infrastructure.Json.NewtonsoftJson;
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
