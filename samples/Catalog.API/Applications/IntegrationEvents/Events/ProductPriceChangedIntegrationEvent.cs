using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events;

namespace Catalog.API.Applications.IntegrationEvents.Events
{
    public class ProductPriceChangedIntegrationEvent : IntegrationEvent
    {

        public ProductPriceChangedIntegrationEvent(string sku, decimal newPrice, decimal oldPrice) : base()
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
