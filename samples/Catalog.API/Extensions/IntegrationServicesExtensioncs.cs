using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
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
            // JSON 转换器已移除，现在使用 System.Text.Json
           
            return services;
        }
    }
}
