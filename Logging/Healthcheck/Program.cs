using Healthcheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("check for database",
        HealthStatus.Degraded, //Degraded will be returned if healthcheck failed 
        tags: new[] { "sample" })
    //tags are used to filter healthcheck, in case you have a lot of healthchecks, like a annual health check 
    .AddCheck("healthcheck defined by a lambda", () => HealthCheckResult.Healthy("A healthy result."));

// Can add other health checks, to separate them from each other for better separation of concerns
//  .AddCheck<ServiceHealthCheck>("Check for service)

var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/", () => "Hello World!");

app.Run();