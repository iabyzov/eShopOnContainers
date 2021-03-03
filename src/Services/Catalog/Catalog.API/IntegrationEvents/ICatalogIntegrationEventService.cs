using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;
using Microsoft.eShopOnContainers.Services.Catalog.API.IntegrationEvents.Events;

namespace Catalog.API.IntegrationEvents
{
    public interface ICatalogIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(ProductPriceChangedIntegrationEvent evt);
    }
}
