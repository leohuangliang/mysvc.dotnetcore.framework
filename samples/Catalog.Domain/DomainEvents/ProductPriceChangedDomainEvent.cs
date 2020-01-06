using System;
using System.Collections.Generic;
using System.Text;
using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;

namespace Catalog.Domain.DomainEvents
{
    public class ProductPriceChangedDomainEvent : IDomainEvent
    {
        public ProductPriceChangedDomainEvent(string sku, decimal newPrice, decimal oldPrice)
        {

            this.SKU = sku;
            this.NewPrice = newPrice;
            this.OldPrice = oldPrice;

        }
        public string SKU { get; private set; }
        public decimal NewPrice { get; private set; }
        public decimal OldPrice { get; private set; }
    }
}
