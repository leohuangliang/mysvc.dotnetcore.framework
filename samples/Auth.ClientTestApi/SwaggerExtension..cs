using System;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Auth.ClientTestApi
{
    /// <summary>
    /// Swagger相关权限的扩展
    /// </summary>
    public static class AuthorizeExtension
    {
        /// <summary>
        /// 增加自定义的Swagger扩展
        /// </summary>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{configuration.GetValue<string>("AuthHostAddress")}/connect/authorize", UriKind.Relative),
                            TokenUrl = new Uri($"{configuration.GetValue<string>("AuthHostAddress")}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "AuthClientTestApi", "Auth.ClientTestApi"},
                                { "clientIdentityApi","client Identity Service Api"}
                            }
                        }
                    },
                  
                });

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Client Service API", Version = "v1" });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPathApi = Path.Combine(basePath, "Auth.ClientTestApi.xml");
                
                options.IncludeXmlComments(xmlPathApi);

                options.OperationFilter<AuthorizeCheckOperationFilter>();


            });

            //配置Swagger
            services.ConfigureSwaggerGen(options =>
            {
                // UseFullTypeNameInSchemaIds replacement for .NET Core
                options.CustomSchemaIds(x => x.FullName);


            });

            return services;
        }

        /// <summary>
        /// Swagger调用时的权限验证
        /// </summary>
        public class AuthorizeCheckOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                // Check for authorize attribute
                var hasAuthorize = context.MethodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Any() ||
                                   context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
                                   context.MethodInfo.GetCustomAttributes(true).OfType<PermissionAttribute>().Any();

                if (hasAuthorize)
                {
                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                    operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                    var oAuthScheme = new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    };
                    operation.Security = new List<OpenApiSecurityRequirement>()
                    {
                        new OpenApiSecurityRequirement
                        {
                            [ oAuthScheme ] =  new [] { "AuthClientTestApi" }
                        }
                    };
                    
                }
            }

           
        }
    }
}
