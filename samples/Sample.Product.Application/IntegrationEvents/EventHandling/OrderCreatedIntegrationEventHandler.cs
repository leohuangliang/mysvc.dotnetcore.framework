using System.Linq;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample.Product.Application.Commands;
using Sample.Product.Application.IntegrationEvents.Events;

namespace Sample.Product.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// 订单已经创建的集成事件处理器
    /// </summary>
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        private ILogger<OrderCreatedIntegrationEventHandler> _logger;

        
        public OrderCreatedIntegrationEventHandler(IMediator mediator, ILogger<OrderCreatedIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Subscribe(nameof(OrderCreatedIntegrationEvent))]
        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            _logger.LogInformation($"Handling {@event.GetType().Name}");
            
            var command = new ReduceProductStockCommand(@event.OrderItems.Select(x => new ReduceProductStockItem(x.SKU, x.Units)).ToList());
            await _mediator.Send(command);
        }
    }
}
