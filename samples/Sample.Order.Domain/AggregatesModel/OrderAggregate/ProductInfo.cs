using MySvc.DotNetCore.Framework.Domain.Core.Impl;

namespace Sample.Order.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// 产品信息
    /// </summary>
    public class ProductInfo : ValueObject<ProductInfo>
    {
        public ProductInfo(string sku, string title)
        {
            SKU = sku;
            Title = title;
        }

        public string SKU { get; private set; }

        public string Title { get; private set; }
    }
}
