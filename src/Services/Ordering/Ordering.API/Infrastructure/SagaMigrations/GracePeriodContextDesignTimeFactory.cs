using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Microsoft.eShopOnContainers.Services.Ordering.API.Infrastructure.SagaMigrations
{
    public class GracePeriodContextDesignTimeFactory : IDesignTimeDbContextFactory<GracePeriodDbContext>
    {
        public GracePeriodDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GracePeriodDbContext>();

            optionsBuilder.UseSqlServer(".", options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

            return new GracePeriodDbContext(optionsBuilder.Options);
        }
    }
}