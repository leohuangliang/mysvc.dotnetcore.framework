using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Extensions
{
    public static  class WaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new  Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "BEF  - Catalog HTTP API",
                    Version = "v1",
                    Description = "The Catalog Microservice HTTP API.",
                });
            });

            return services;

        }
    }
}
