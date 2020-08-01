using System;
using System.Collections.Generic;

namespace Contracts.Events
{
    /// <summary>
    /// 订单创建成功的集成事件
    /// </summary>
    public class OrderCreatedIntegrationEvent
    {


        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 订单项
        /// </summary>
        public OrderItem[] OrderItems { get; set; }

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
