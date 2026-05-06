
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RecordShop
{
    // Source - https://stackoverflow.com/a/76481253
    // Posted by mostafa, modified by community. See post 'Timeline' for change history
    // Retrieved 2026-05-06, License - CC BY-SA 4.0

    public class DbContextHealthCheck<TContext> : IHealthCheck where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public DbContextHealthCheck(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Database.CanConnectAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("DbContext connection test failed", ex);
            }
        }
    }

}
