using Microsoft.AspNetCore.SignalR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.SignalrHub.IntegrationEvents.EventHandling
{
    public class OrderStatusChangedToSubmittedIntegrationEventHandler :
        IntegrationEventHandlerBase<OrderStatusChangedToSubmittedIntegrationEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<OrderStatusChangedToSubmittedIntegrationEventHandler> _logger;

        public OrderStatusChangedToSubmittedIntegrationEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<OrderStatusChangedToSubmittedIntegrationEventHandler> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public override async Task Handle(OrderStatusChangedToSubmittedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                await _hubContext.Clients
                    .Group(@event.BuyerName)
                    .SendAsync("UpdatedOrderState", new { OrderId = @event.OrderId, Status = @event.OrderStatus });
            }
        }
    }
}