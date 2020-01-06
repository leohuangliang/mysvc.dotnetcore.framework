using System.Collections.Generic;
using MediatR;
using Sample.Order.Application.Common.Models;
using Sample.Order.Application.ViewModels;

namespace Sample.Order.Application.Commands
{
    /// <summary>
    /// 创建产品的命令
    /// </summary>
    public class CreateOrderCommand : IRequest<ViewModels.Order>, ICommand
    {
        /// <summary>
        /// 买家
        /// </summary>
        public string Buyer { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// 订单项
        /// </summary>
        public IList<OrderItemDTO> OrderItems { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class OrderItemDTO
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
        /// 单位价格
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// 单位数量
        /// </summary>
        public int Units { get; set; }
    }
}
