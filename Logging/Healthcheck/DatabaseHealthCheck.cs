using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Healthcheck;

/*
 * Each healthcheck can be used to check for external dependencies like database, server, or something else.
 * You can have multiple healthchecks to separate them from each other.
 */
public class DatabaseHealthCheck: IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var isHealthy = true;
        //check for external dependencies (like servers, DBs, etc)
        //if one is not responsive, the service is not healthy

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Database is up and running"));
        }

        return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Database failed to respond"));
    }
}