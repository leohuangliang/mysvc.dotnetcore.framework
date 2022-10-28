using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Catalog.API.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Extensions
{
    public static class CustomMVCExtension
    {
        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHealthChecks(checks =>
            //{
            //    var minutes = 1;
            //    if (int.TryParse(configuration["HealthCheck:Timeout"], out var minutesParsed))
            //    {
            //        minutes = minutesParsed;
            //    }
            //    checks.AddSqlCheck("CatalogDb", configuration["ConnectionString"], TimeSpan.FromMinutes(minutes));

            //    var accountName = configuration.GetValue<string>("AzureStorageAccountName");
            //    var accountKey = configuration.GetValue<string>("AzureStorageAccountKey");
            //    if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
            //    {
            //        checks.AddAzureBlobStorageCheck(accountName, accountKey);
            //    }
            //});


            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddNewtonsoftJson()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:5000",
                                                   "https://localhost:5001")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
        }
    }
}
