using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.IdGenerators;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator
{
    public class SnowflakeBackgroundServices : BackgroundService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IDistributedSupport _distributed;
        private readonly SnowflakeOption option;
        public SnowflakeBackgroundServices(IIdGenerator idGenerator, IDistributedSupport distributed, IOptions<SnowflakeOption> options)
        {
            _idGenerator = idGenerator;
            option = options.Value;
            _distributed = distributed;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                while (true)
                {
                    //定时刷新机器id的存活状态
                    await _distributed.RefreshAlive();
                    await Task.Delay(option.RefreshAliveInterval.Add(TimeSpan.FromMinutes(1)), stoppingToken);
                }

            }
        }
    }
}