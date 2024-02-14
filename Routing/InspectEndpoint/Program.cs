var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    var currentEndpoint = context.GetEndpoint();

    Console.WriteLine("Inspecting endpoint {0}", currentEndpoint.DisplayName);

    if(currentEndpoint is RouteEndpoint routeEndpoint)
    {
        Console.WriteLine("Route pattern: {0}", routeEndpoint.RoutePattern.RawText);
    }

    foreach(var endpointMetadata in currentEndpoint.Metadata)
    {
        Console.WriteLine("Metadata: {0}", endpointMetadata);
    }

    await next.Invoke();
});

app.MapGet("/", () => "Inspect Endpoint.");
app.MapGet("/foo/", () => "Inspect Endpoint2.");

app.Run();