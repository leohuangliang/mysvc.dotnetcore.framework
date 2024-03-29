﻿using MySvc.Framework.Domain.Core.DomainEvents;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;
using Sample.Order.Domain.Events;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Events;

namespace Sample.Order.Application.DomainEventHandlers
{
    /// <summary>
    /// OrderCreatedDomainEvent领域事件的处理器
    /// </summary>
    public class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public OrderCreatedDomainEventHandler(IIntegrationEventService integrationEventService)
        {

            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }

        public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var order = domainEvent.Order;
            //处理领域相关的事情

            //保存集成事件

            var orderItems = order.OrderItems.Select(x => new OrderItem
            {
                SKU = x.Product.SKU,
                Title = x.Product.Title,
                Units = x.Units
            }).ToArray();
            await _integrationEventService.SaveIntegrationEvent<OrderCreatedIntegrationEvent>(
                new OrderCreatedIntegrationEvent
                {
                    OrderNo = order.OrderNo,
                    OrderTime = order.OrderTime,
                    OrderItems = orderItems
                });
        }
    }
}
