using System;
using MySvc.DotNetCore.Framework.Domain.Core.Attributes;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;
using Catalog.Domain.DomainEvents;

namespace Catalog.Domain
{
    [AggregateRootName("products")]
    public class Product : AggregateRoot
    {
        public Product(string sku)
        {
            this.SKU = sku;
        }

        //testtest

        public string SKU { get; private set; }
        public string HeadLine { get; set; }
        public decimal Price { get; private set; }

        public void SetPrice(decimal price)
        {
            if (!this.IsTransient())
            {
                if (this.Price != price)
                {
                    AddDomainEvent(new ProductPriceChangedDomainEvent(this.SKU,price, this.Price));
                }
            }
            this.Price = price;
        }


    }
}
