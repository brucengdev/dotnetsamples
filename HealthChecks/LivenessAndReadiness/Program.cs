using LivenessAndReadiness;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<BackgroundTaskService>();
builder.Services.AddSingleton<MyHealthCheck>();
//adding the healthcheck as a singleton allows resolving it from other classes

builder.Services.AddHealthChecks()
    .AddCheck<MyHealthCheck>("Readiness check", tags: new[] { "ready" });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false //simulate always live
});

app.Run();