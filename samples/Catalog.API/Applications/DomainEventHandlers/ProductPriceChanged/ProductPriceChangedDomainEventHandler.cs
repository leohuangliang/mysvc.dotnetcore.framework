using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core.DomainEvents;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;
using Catalog.API.Applications.IntegrationEvents.Events;
using Catalog.Domain.DomainEvents;

namespace Catalog.API.Applications.DomainEventHandlers.ProductPriceChanged
{
    public class ProductPriceChangedDomainEventHandler : IDomainEventHandler<ProductPriceChangedDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public ProductPriceChangedDomainEventHandler(IIntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }


        public async Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
        {

            await _integrationEventService.PublishIntegrationEventWithoutSave<ProductPriceChangedIntegrationEvent>(
                    new ProductPriceChangedIntegrationEvent
                    {
                        SKU = notification.SKU,
                        NewPrice = notification.NewPrice,
                        OldPrice = notification.OldPrice
                    });

        }
    }
}
