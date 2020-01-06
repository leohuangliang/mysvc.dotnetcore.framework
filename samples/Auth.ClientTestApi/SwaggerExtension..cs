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
using Operation = Swashbuckle.AspNetCore.Swagger.Operation;

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
                
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{configuration.GetValue<string>("AuthHostAddress")}/connect/authorize",
                    TokenUrl = $"{configuration.GetValue<string>("AuthHostAddress")}/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "AuthClientTestApi", "Auth.ClientTestApi"},
                        { "clientIdentityApi","client Identity Service Api"}

                    }
                });

                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info { Title = "Client Service API", Version = "v1" });

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
            public void Apply(Operation operation, OperationFilterContext context)
            {
                // Check for authorize attribute
                var hasAuthorize = context.ApiDescription.ControllerAttributes().OfType<AuthorizeAttribute>().Any() ||
                                   context.ApiDescription.ActionAttributes().OfType<AuthorizeAttribute>().Any() ||
                                   context.ApiDescription.ActionAttributes().OfType<PermissionAttribute>().Any();

                if (hasAuthorize)
                {
                    operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                    operation.Responses.Add("403", new Response { Description = "Forbidden" });

                    operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                    operation.Security.Add(new Dictionary<string, IEnumerable<string>>
                    {
                        { "oauth2", new [] { "AuthClientTestApi" } }
                    });
                }
            }
        }
    }
}
