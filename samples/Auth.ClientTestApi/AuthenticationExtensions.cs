using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.ClientTestApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// 增加自定义权限
        /// </summary>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration.GetValue<string>("AuthHostAddress");
                options.RequireHttpsMetadata = false;
                options.ApiName = "AuthClientTestApi";
            });

            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = configuration.GetValue<string>("RedisCacheConnectionString");
            //});

            //services.Configure<PermissionsAuthorizationOptions>(configuration.GetSection("AuthorizationOptions"));
            //services.AddHttpClient<AuthHttpClient>();

            return services;
        }
    }
}
