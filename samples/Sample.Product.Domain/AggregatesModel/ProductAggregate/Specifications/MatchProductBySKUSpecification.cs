using System;
using System.Linq.Expressions;
using MySvc.Framework.Domain.Core.Specification;

namespace Sample.Product.Domain.AggregatesModel.ProductAggregate.Specifications
{
    /// <summary>
    /// 通过产品SKU匹配产品信息的规格
    /// </summary>
    public class MatchProductBySKUSpecification : Specification<Product>
    {
        private readonly string _sku;

        public MatchProductBySKUSpecification(string sku)
        {
            _sku = sku ?? string.Empty;
        }
        
        public override Expression<Func<Product, bool>> GetExpression()
        {
            return x => x.SKU == _sku;
        }
    }
}
