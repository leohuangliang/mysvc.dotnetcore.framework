using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MySvc.DotNetCore.Framework.Domain.Core.Specification;

namespace Catalog.Domain.Specifications
{
    public class MatchProductSKUSpecification : Specification<Product>
    {
        public string SKU { get; private set; }

        public MatchProductSKUSpecification(string sku)
        {
            this.SKU = sku;
        }

        public override Expression<Func<Product, bool>> GetExpression()
        {
            return item => item.SKU == this.SKU;
        }
    }
}
