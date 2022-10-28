using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Catalog.API.Extensions
{
    public static class MongoDBContextExtensions
    {
        public static IWebHost MigrateMongoDBContext(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var maongoDbManager = services.GetService<MongoDBManager>();
                var logger = services.GetRequiredService<ILogger<MongoDBManager>>();

                try
                {
                    logger.LogInformation($"Migrating database");


                    //创建集合
                    maongoDbManager.CreateCollections();


                    logger.LogInformation($"Migrated database associated");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database");
                }
            }

            return webHost;
        }

        public static IServiceCollection AddCustomMongoDBContext(this IServiceCollection services)
        {
            services.AddSingleton<MongoDBManager>();
            services.AddScoped<IDBContext, MongoDBContext>();
            services.AddScoped<IMongoDBContext, MongoDBContext>();

            return services;
        }
    }
}
