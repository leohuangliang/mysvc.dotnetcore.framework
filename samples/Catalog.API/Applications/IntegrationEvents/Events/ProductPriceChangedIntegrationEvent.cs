using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Applications.IntegrationEvents.Events
{
    public class ProductPriceChangedIntegrationEvent
    {
        public string SKU { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
    }
}
