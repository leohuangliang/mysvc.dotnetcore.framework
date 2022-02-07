using Microsoft.Extensions.DependencyInjection;
using MySvc.Framework.Infrastructure.Crosscutting.IdGenerators;

namespace MySvc.Framework.Infrastructure.Crosscutting.SnowflakeIdGenerator
{
    public static class SnowflakeDependencyInjection
    {
        public static IServiceCollection AddSnowflake(this IServiceCollection service, Action<SnowflakeOption> option)
        {
            service.Configure(option);
            service.AddSingleton<IIdGenerator, SnowflakeIdGenerator>();
            return service;
        }
    }
}