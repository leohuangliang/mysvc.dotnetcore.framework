using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using Sample.Order.Application.IntegrationEvents.Events;
using Sample.Order.Domain.Events;

namespace Sample.Order.Application.DomainEventHandlers
{
    /// <summary>
    /// OrderCreatedDomainEvent领域事件的处理器
    /// </summary>
    public class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public OrderCreatedDomainEventHandler(IEventBus eventBus)
        {
            
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }
        
        public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var order = domainEvent.Order;
            //处理领域相关的事情

            //保存集成事件
            await _eventBus.Publish(
                new OrderCreatedIntegrationEvent(order.OrderNo, order.OrderTime, 
                    order.OrderItems.Select(x => new OrderItem()
                    {
                        SKU = x.Product.SKU,
                        Title = x.Product.Title,
                        Units = x.Units
                    }).ToList()));
        }
    }
}
