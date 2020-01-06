namespace Sample.Order.Application.ViewModels
{
    /// <summary>
    /// 订单项
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// 产品信息
        /// </summary>
        public ProductInfo Product { get; set; }

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
