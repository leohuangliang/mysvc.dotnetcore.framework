using Microsoft.Extensions.DependencyInjection;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.IdGenerators;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator
{
    public static class SnowflakeDependencyInjection
    {
        public static IServiceCollection AddSnowflake(this IServiceCollection service, Action<SnowflakeOption> option)
        {
            service.Configure(option);
            service.AddSingleton<IIdGenerator, IdGenerator.SnowflakeIdGenerator.SnowflakeIdGenerator>();
            return service;
        }
    }
}