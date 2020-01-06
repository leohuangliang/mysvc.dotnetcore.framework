using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;

namespace Sample.Order.Domain.Events
{
    /// <summary>
    /// 订单已创建的领域事件
    /// </summary>
    public class OrderCreatedDomainEvent : IDomainEvent
    {
        public OrderCreatedDomainEvent(AggregatesModel.OrderAggregate.Order order)
        {
            Order = order;
        }

        /// <summary>
        /// 订单
        /// </summary>
        public AggregatesModel.OrderAggregate.Order Order { get; }
    }
}
