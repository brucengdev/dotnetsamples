using Healthcheck;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("check for database",
        HealthStatus.Degraded, //Degraded will be returned if healthcheck failed 
        tags: new[] { "sample" })
    //tags are used to filter healthcheck, in case you have a lot of healthchecks, like a annual health check 
    .AddCheck("healthcheck defined by a lambda", () =>
    {
        return HealthCheckResult.Healthy("A healthy result.");
    })
    .AddTypeActivatedCheck<SampleHealthCheckWithArgs>(
        "Health check with args",
        HealthStatus.Unhealthy,
        tags: new List<string> { "tag1", "tag2"}, 1, "test");

// Can add other health checks, to separate them from each other for better separation of concerns
//  .AddCheck<ServiceHealthCheck>("Check for service)

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions()
    {
        //only run healthchecks with tag tag1
        Predicate = healthCheck => healthCheck.Tags.Contains("tag1"),
        //customize the http status code depending on health check result
        ResultStatusCodes =
        {
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Degraded] = StatusCodes.Status200OK,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        },
        AllowCachingResponses = false//if this is false, the middlewares add cache-control header to response
        //to prevent response caching of health probes.s    
    })
    .RequireHost("localhost:5103"); //healthchecks will only be in effect if the host is as specified
    //.RequireAuthorization();//use RequireAuthorization to allow only authenticated users to access healthchecks.
//.RequireCors();//it's not common to run healthchecks from a browser, but if so, use RequireCors

app.MapGet("/", () => "Hello World!");

app.Run();