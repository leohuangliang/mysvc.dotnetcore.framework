using System;
using System.Linq.Expressions;
using MySvc.Framework.Domain.Core.Specification;

namespace Sample.Product.Domain.AggregatesModel.ProductAggregate.Specifications
{
    /// <summary>
    /// 通过产品包含指定标题信息而匹配产品信息的规格
    /// </summary>
    public class MatchProductByContainsTitleSpecification : Specification<Product>
    {
        private readonly string _title;

        public MatchProductByContainsTitleSpecification(string sku)
        {
            _title = sku ?? string.Empty;
        }
        
        public override Expression<Func<Product, bool>> GetExpression()
        {
            return x => x.Title.Contains(_title);
        }
    }
}
