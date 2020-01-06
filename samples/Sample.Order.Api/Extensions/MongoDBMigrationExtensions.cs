using System;
using System.Data.SqlClient;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
        public static IWebHost MigrateMongoDB(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var maongoDbManager = services.GetService<MongoDBManager>();
                var logger = services.GetRequiredService<ILogger<MongoDBManager>>();

                try
                {
                    logger.LogInformation($"Migrating database");

                    
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

    }
}
