using System.Collections.Generic;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Ordering.API.Application.Sagas;

namespace Microsoft.eShopOnContainers.Services.Ordering.API.Infrastructure
{
    public class GracePeriodDbContext : SagaDbContext
    {
        public GracePeriodDbContext(DbContextOptions<GracePeriodDbContext> options) : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new GracePeriodMap();
            }
        }
    }
}