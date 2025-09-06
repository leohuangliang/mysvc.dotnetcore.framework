using System;
using System.Collections.Generic;
using Sample.Order.Application.Common.Models;

namespace Sample.Order.Application.ViewModels
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public required string OrderNo { get; set; }

        /// <summary>
        /// 买家
        /// </summary>
        public required string Buyer { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public required Address Address { get; set; }

        /// <summary>
        /// 订单项
        /// </summary>
        public required IList<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public required string Remark { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; }
    }
}
