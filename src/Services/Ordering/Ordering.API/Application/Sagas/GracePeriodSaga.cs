using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Automatonymous;
using MassTransit;
using Microsoft.eShopOnContainers.Services.Catalog.API.IntegrationEvents.Events;
using Ordering.API.Application.IntegrationEvents.Events;
using Payment.API.IntegrationEvents.Events;

namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Sagas
{
    public class GraceState :
        SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        
        public int OrderId { get; set; }

        public List<OrderStockItem> OrderStockItems { get; set; } = new List<OrderStockItem>();
        
        public Guid? GracePeriodTimeoutTokenId { get; set; }
    }

    public class GracePeriodExpired
    {
        public Guid OrderId { get; set; }
    }

    public class GracePeriodMachine : MassTransitStateMachine<GraceState>
    {
        public State Submitted { get; private set; }
        public State Accepted { get; private set; }

        public State Started { get; private set; }

        public GracePeriodMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderStarted, x => x.CorrelateById(context => context.Message.Id));
            
            Schedule(() => GracePeriodExpired, x => x.GracePeriodTimeoutTokenId,
                x =>
                {
                    x.Delay = TimeSpan.FromSeconds(30);
                    x.Received = configurator => configurator.CorrelateById(context => context.Message.OrderId);
                });

            Initially(
                When(OrderStarted)
                    .Then(Initiate)
                    .Schedule(GracePeriodExpired, context => context.Init<GracePeriodExpired>(new GracePeriodExpired() {OrderId = context.Instance.CorrelationId}))
                    .TransitionTo(Started));

            During(Started,
                When(GracePeriodExpired.Received)
                    .ThenAsync(DispatchValidation));

        }

        private static void Initiate(BehaviorContext<GraceState, OrderStartedIntegrationEvent> context)
        {
            context.Instance.OrderStockItems = context.Data.OrderedItems;
            context.Instance.OrderId = context.Data.OrderId;
        }

        private Task DispatchValidation(BehaviorContext<GraceState, GracePeriodExpired> context)
        {
            var @event = new OrderStatusChangedToAwaitingValidationIntegrationEvent(context.Instance.OrderId, context.Instance.OrderStockItems);
            return context.Publish(@event);
        }

        public Event<OrderStartedIntegrationEvent> OrderStarted { get; private set; }
        // public Event<OrderStockConfirmedIntegrationEvent> OrderStockConfirmed { get; private set; }
        // public Event<OrderStockRejectedIntegrationEvent> OrderStockRejected { get; private set; }
        // public Event<OrderPaymentSucceededIntegrationEvent> OrderPaymentSucceeded { get; private set; }
        // public Event<OrderPaymentFailedIntegrationEvent> OrderPaymentFailed { get; private set; }
        // public Event<OrderStatusChangedToCancelledIntegrationEvent> OrderCancelled { get; private set; }
        
        public Schedule<GraceState, GracePeriodExpired> GracePeriodExpired { get; private set; }
    }
}