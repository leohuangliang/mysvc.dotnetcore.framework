using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Cap
{
    public class CapEventBus : IEventBus, IDisposable
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ILogger<CapEventBus> _logger;

        public CapEventBus(ICapPublisher capPublisher, ILogger<CapEventBus> logger)
        {
            _capPublisher = capPublisher ?? throw new ArgumentNullException(nameof(capPublisher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Publish(IIntegrationEvent @event)
        {
            await _capPublisher.PublishAsync(@event.GetType().Name, @event);
        }

        public void Dispose()
        {

        }
    }
}
