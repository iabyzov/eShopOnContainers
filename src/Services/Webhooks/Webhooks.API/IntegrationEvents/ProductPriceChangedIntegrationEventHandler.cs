using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;
using Microsoft.eShopOnContainers.Services.Catalog.API.IntegrationEvents.Events;

namespace Webhooks.API.IntegrationEvents
{
    public class ProductPriceChangedIntegrationEventHandler : IntegrationEventHandlerBase<ProductPriceChangedIntegrationEvent>
    {
        public override async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            int i = 0;
        }
    }
}
