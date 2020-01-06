using System;
using System.Collections.Generic;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events;

namespace Sample.Order.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 订单创建成功的集成事件
    /// </summary>
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public OrderCreatedIntegrationEvent(string orderNo, DateTime orderTime, IList<OrderItem> orderItems)
        {
            OrderNo = orderNo;
            OrderTime = orderTime;
            OrderItems = orderItems;
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; private set; }
 
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; private set; }
  
        /// <summary>
        /// 订单项
        /// </summary>
        public IList<OrderItem> OrderItems { get; private set; }
    }

    public class OrderItem
    {
        /// <summary>
        /// 产品SKU
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// 产品标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 单位数量
        /// </summary>
        public int Units { get; set; }
    }
}
