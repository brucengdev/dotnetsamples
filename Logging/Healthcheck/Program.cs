using Healthcheck;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("check for database");
// Can add other health checks, to separate them from each other for better separation of concerns
//  .AddCheck<ServiceHealthCheck>("Check for service)

var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/", () => "Hello World!");

app.Run();