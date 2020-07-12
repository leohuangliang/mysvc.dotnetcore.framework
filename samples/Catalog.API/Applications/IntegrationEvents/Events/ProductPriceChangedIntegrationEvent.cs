using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Applications.IntegrationEvents.Events
{
    public interface IProductPriceChangedIntegrationEvent
    {

        string SKU { get; }
        decimal NewPrice { get; }
        decimal OldPrice { get; }
    }
}
