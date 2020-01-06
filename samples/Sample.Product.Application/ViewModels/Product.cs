namespace Sample.Product.Application.ViewModels
{
    /// <summary>
    /// 产品
    /// </summary>
    public class Product
    {
        /// <summary>
        /// SKU 唯一标识
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// 产品Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQty { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string Desc { get; set; }
    }
}
