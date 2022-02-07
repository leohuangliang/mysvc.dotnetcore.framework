using Microsoft.Extensions.DependencyInjection;
using MySvc.Framework.Infrastructure.Crosscutting.IdGenerators;

namespace MySvc.Framework.Infrastructure.Crosscutting.SnowflakeIdGenerator.Redis
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