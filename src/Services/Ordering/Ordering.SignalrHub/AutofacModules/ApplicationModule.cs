using Autofac;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Ordering.SignalrHub.IntegrationEvents;
using System.Reflection;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.SignalrHub.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(OrderStatusChangedToAwaitingValidationIntegrationEvent).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IntegrationEventHandlerBase<>));

        }
    }
}
