using System;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Sample.Order.Api.Extensions
{
    /// <summary>
    /// Mongodb 数据库迁移相关的扩展
    /// </summary>
    public static class MongoDBMigrationExtensions
    {
        /// <summary>
        /// 初始化MongoDB的Collection
        /// </summary>
        public static IHost MigrateMongoDB(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var mongoDbManager = services.GetService<MongoDBManager>();
                var logger = services.GetRequiredService<ILogger<MongoDBManager>>();

                try
                {
                    logger.LogInformation($"Migrating database");

                    mongoDbManager.CreateCollections();

                    logger.LogInformation($"Migrated database associated");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database");
                }
            }

            return webHost;
        }

    }
}
