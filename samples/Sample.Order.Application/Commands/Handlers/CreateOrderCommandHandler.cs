﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using MediatR;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using Sample.Order.Application.Extensions;
using Sample.Order.Domain.AggregatesModel.OrderAggregate;
using Sample.Order.Domain.Repositories;

namespace Sample.Order.Application.Commands.Handlers
{
    /// <summary>
    /// 创建订单的处理器
    /// </summary>
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ViewModels.Order>
    {
        private readonly IDBContext _dbContext;

        private readonly IOrderRepository _orderRepository;

        private readonly ITypeAdapter _typeAdapter;
        private readonly IIntegrationEventService _integrationEventService;


        public CreateOrderCommandHandler(IDBContext dbContext, IOrderRepository orderRepository, ITypeAdapter typeAdapter, IIntegrationEventService integrationEventService)
        {
            _dbContext = dbContext;
            _orderRepository = orderRepository;
            _typeAdapter = typeAdapter;
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }

        public async Task<ViewModels.Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {

            _dbContext.BeginTransaction();

            var orderItemList = new List<OrderItem>();
            if (command.OrderItems != null)
            {
                foreach (var commandOrderItem in command.OrderItems)
                {
                    orderItemList.Add(new OrderItem(new ProductInfo(commandOrderItem.SKU, commandOrderItem.Title), commandOrderItem.UnitPrice, commandOrderItem.Discount, commandOrderItem.Units));
                }
            }

            var order = new Domain.AggregatesModel.OrderAggregate.Order(command.Buyer, command.Address.ToDomain(), orderItemList);

            await _orderRepository.AddAsync(order);
            await _dbContext.CommitAsync();
            await _integrationEventService.PublishAllAsync();
            return _typeAdapter.Adapt<ViewModels.Order>(order);
        }
    }
}
