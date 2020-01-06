using System;
using System.Collections.Generic;
using System.Linq;
using MySvc.DotNetCore.Framework.Domain.Core.Attributes;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;
using Sample.Order.Domain.Common.Models;
using Sample.Order.Domain.Events;

namespace Sample.Order.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// 订单
    /// </summary>
    [AggregateRootName("Orders")]
    public class Order : AggregateRoot
    {
        private Order()
        {
            Address = new Address("", "", "", "", "", "", "");

            _orderItems = new List<OrderItem>();
        }
        public Order(string buyer, Address address, IList<OrderItem> orderItems)
        {
            Buyer = buyer;
            Address = address;
            OrderNo = Guid.NewGuid().ToString("N");
            _orderItems = orderItems.ToList();

            OrderTime = DateTime.UtcNow;

            this.AddDomainEvent(new OrderCreatedDomainEvent(this));
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; private set; }

        /// <summary>
        /// 买家
        /// </summary>
        public string Buyer { get; private set; }

        /// <summary>
        /// 地址
        /// </summary>
        public Address Address { get; private set; }

        private List<OrderItem> _orderItems;

        /// <summary>
        /// 订单项
        /// </summary>
        public IReadOnlyCollection<OrderItem> OrderItems
        {
            get => _orderItems.AsReadOnly();
            private set => _orderItems = value.ToList();
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get;set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; private set; }
    }
}



