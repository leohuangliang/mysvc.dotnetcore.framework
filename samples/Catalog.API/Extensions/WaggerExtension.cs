using System;
using System.Collections.Generic;
using System.Linq;
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
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
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
