using Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample.Product.Application.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Product.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// 订单已经创建的集成事件处理器
    /// </summary>
    public class OrderCreatedIntegrationEventHandler : IConsumer<OrderCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        private ILogger<OrderCreatedIntegrationEventHandler> _logger;


        public OrderCreatedIntegrationEventHandler(IMediator mediator, ILogger<OrderCreatedIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            _logger.LogInformation($"Handling {context.Message.GetType().Name}");

            var command = new ReduceProductStockCommand(context.Message.OrderItems.Select(x => new ReduceProductStockItem(x.SKU, x.Units)).ToList());
            await _mediator.Send(command);
        }
    }
}
