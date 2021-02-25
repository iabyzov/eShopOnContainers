using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;
using MassTransit;

namespace Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions
{
    public abstract class IntegrationEventHandlerBase<TIntegrationEvent> : 
        IIntegrationEventHandler<TIntegrationEvent>, 
        IConsumer<TIntegrationEvent> 
        where TIntegrationEvent: IntegrationEvent
    {
        public abstract Task Handle(TIntegrationEvent @event);

        public Task Consume(ConsumeContext<TIntegrationEvent> context)
        {
            var @event = context.Message;
            return Handle(@event);
        }
    }

    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
