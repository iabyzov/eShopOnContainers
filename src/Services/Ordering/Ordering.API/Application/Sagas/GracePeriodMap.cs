using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Sagas
{
    public class GracePeriodMap : SagaClassMap<GraceState>
    {
        protected override void Configure(EntityTypeBuilder<GraceState> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(64);
        }
    }
}