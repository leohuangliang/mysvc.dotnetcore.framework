using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySvc.Framework.IS4.MongoDB.Options;
using MySvc.Framework.IS4.MongoDB.Services;
using MySvc.Framework.IS4.MongoDB.Stores;
using System;

namespace MySvc.Framework.IS4.MongoDB
{
    public static class IdentityServerMongoDbBuilderExtensions
    {

        public static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder)
        {
            builder.Services.AddScoped<IClientStore, ClientStore>();
            builder.Services.AddScoped<IResourceStore, ResourceStore>();
            builder.Services.AddScoped<IPersistedGrantStore, PersistedGrantStore>();
            builder.Services.AddScoped<ICorsPolicyService, CorsPolicyService>();
            return builder;
        }


        public static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder,
            Action<TokenCleanupOptions> tokenCleanUpOptions = null)
        {
            var tokenCleanupOptions = new TokenCleanupOptions();
            tokenCleanUpOptions?.Invoke(tokenCleanupOptions);
            builder.Services.AddSingleton(tokenCleanupOptions);
            builder.Services.AddSingleton<TokenCleanup>();

            return builder;
        }

        public static IApplicationBuilder UseIdentityServerMongoDbTokenCleanup(this IApplicationBuilder app, IHostApplicationLifetime applicationLifetime)
        {
            var tokenCleanup = app.ApplicationServices.GetService<TokenCleanup>();
            if (tokenCleanup == null)
            {
                throw new InvalidOperationException("AddOperationalStore must be called on the service collection.");
            }
            applicationLifetime.ApplicationStarted.Register(tokenCleanup.Start);
            applicationLifetime.ApplicationStopping.Register(tokenCleanup.Stop);

            return app;
        }
    }
}
