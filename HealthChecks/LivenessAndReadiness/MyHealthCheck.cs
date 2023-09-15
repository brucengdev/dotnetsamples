using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LivenessAndReadiness;

public class MyHealthCheck: IHealthCheck
{
    private volatile bool _isReady;
    public bool IsReady
    {
        get => _isReady;
        set => _isReady = value;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        if (IsReady)
        {
            return HealthCheckResult.Healthy("A healthy result, the service is ready");
        }

        return HealthCheckResult.Unhealthy("An unhealthy result, the service is not yet ready");
    }
}