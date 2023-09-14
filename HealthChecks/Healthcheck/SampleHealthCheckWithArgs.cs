using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Healthcheck;

public class SampleHealthCheckWithArgs: IHealthCheck
{
    private int firstArg;
    private string secondArg;
    public SampleHealthCheckWithArgs(int arg0, string arg1)
    {
        //you can pass arguments to a health check implementation using
        //AddTypeActivatedCheck()
        firstArg = arg0;
        secondArg = arg1;
    }
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, 
            $"A healthy result, arg0 = {firstArg}, arg1 = {secondArg}"));
    }
}