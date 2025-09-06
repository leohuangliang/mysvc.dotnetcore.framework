using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Domain.Core.Impl;

namespace Sample.Product.Domain.AggregatesModel.ProductAggregate
{
    /// <summary>
    /// 产品
    /// </summary>
    [AggregateRootName("Products")]
    public class Product : AggregateRoot
    {
        private Product()
        {
            SKU = string.Empty;
            Title = string.Empty;
            Desc = string.Empty;
            Mark = string.Empty;
        }

        public Product(string sku, string title, int stockQty)
        {
            SKU = sku;
            Title = title;
            StockQty = stockQty;
            Mark = "sdfdfs";
        }

        /// <summary>
        /// SKU 唯一标识
        /// </summary>
        public string SKU { get; private set; } = string.Empty;

        /// <summary>
        /// 产品Title
        /// </summary>
        public string Title { get; private set; } = string.Empty;

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQty { get; private set; }
        
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Desc { get; set; } = string.Empty;
        
        public string Mark { get; init; } = string.Empty;

        public void ChangeTitle(string title)
        {
            this.Title = title;
        }

        public void ChangeStockQty(int stockQty)
        {
            this.StockQty = stockQty;
        }

        public void DeductingStockQty(int stockQty)
        {
            this.StockQty = this.StockQty - stockQty;
        }

    }
}
