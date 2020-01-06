using MySvc.DotNetCore.Framework.Domain.Core.Impl;
using System;

namespace Sample.Order.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// 订单项
    /// </summary>
    public class OrderItem : Entity
    {
        public OrderItem(ProductInfo product, decimal unitPrice, decimal discount, int units)
        {
            Product = product;
            UnitPrice = unitPrice;
            Discount = discount;
            Units = units;
        }

        /// <summary>
        /// 产品信息
        /// </summary>
        public ProductInfo Product { get; private set; }

        /// <summary>
        /// 单位价格
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// 单位数量
        /// </summary>
        public int Units { get; private set; }

        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new Exception("Discount is not valid");
            }

            Discount = discount;
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new Exception("Invalid units");
            }

            Units += units;
        }
    }
}
