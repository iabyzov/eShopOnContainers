using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Ordering.API.Application.IntegrationEvents.Events;
using Webhooks.API.Model;
using Webhooks.API.Services;

namespace Webhooks.API.IntegrationEvents
{
    public class OrderStatusChangedToPaidIntegrationWebhooksEventHandler : IntegrationEventHandlerBase<OrderStatusChangedToPaidIntegrationEvent>
    {
        private readonly IWebhooksRetriever _retriever;
        private readonly IWebhooksSender _sender;
        private readonly ILogger _logger;
        public OrderStatusChangedToPaidIntegrationWebhooksEventHandler(IWebhooksRetriever retriever, IWebhooksSender sender, ILogger<OrderStatusChangedToShippedIntegrationWebhooksEventHandler> logger )
        {
            _retriever = retriever;
            _sender = sender;
            _logger = logger;
        }

        public override async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
        {
            var subscriptions = await _retriever.GetSubscriptionsOfType(WebhookType.OrderPaid);
            _logger.LogInformation("Received OrderStatusChangedToShippedIntegrationEvent and got {SubscriptionsCount} subscriptions to process", subscriptions.Count());
            var whook = new WebhookData(WebhookType.OrderPaid, @event);
            await _sender.SendAll(subscriptions, whook);
        }
    }
}
