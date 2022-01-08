using Microsoft.Extensions.DependencyInjection;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.IdGenerators;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis
{
    public static class SnowflakeDependencyInjection
    {
        public static IServiceCollection AddSnowflakeWithRedis(this IServiceCollection service, Action<RedisOption> option)
        {
            service.Configure(option);
            service.AddSingleton<IIdGenerator, SnowflakeIdGenerator>();
            service.AddSingleton<IRedisClient, RedisClient>();
            service.AddSingleton<IDistributedSupport, DistributedSupportWithRedis>();
            service.AddHostedService<SnowflakeBackgroundServices>();
            return service;
        }
    }
}