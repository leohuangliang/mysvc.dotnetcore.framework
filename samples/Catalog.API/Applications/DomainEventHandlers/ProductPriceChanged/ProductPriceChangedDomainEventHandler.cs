using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using Catalog.API.Applications.IntegrationEvents.Events;
using Catalog.Domain.DomainEvents;

namespace Catalog.API.Applications.DomainEventHandlers.ProductPriceChanged
{
    public class ProductPriceChangedDomainEventHandler : IDomainEventHandler<ProductPriceChangedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public ProductPriceChangedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }


        public async Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _eventBus.Publish(
                new ProductPriceChangedIntegrationEvent
                    (notification.SKU,notification.NewPrice,notification.OldPrice));
            
        }
    }
}
