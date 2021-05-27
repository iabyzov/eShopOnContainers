using System.Collections.Generic;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    // Integration Events notes: 
    // An Event is “something that has happened in the past”, therefore its name has to be   
    // An Integration Event is an event that can cause side effects to other microsrvices, Bounded-Contexts or external systems.
    public record OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }

        public int OrderId { get; set; }

        public List<OrderStockItem> OrderedItems { get; set; }

        public OrderStartedIntegrationEvent(string userId, int orderId, List<OrderStockItem> orderStockItems)
        {
            UserId = userId;
            OrderId = orderId;
            OrderedItems = orderStockItems;
        }
    }
}