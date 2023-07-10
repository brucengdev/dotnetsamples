using FactoryBasedMiddleware;

var builder = WebApplication.CreateBuilder(args);

//you must register the Factory based middleware as a service
builder.Services.AddTransient<FactoryActivatedMiddleware>();
builder.Services.AddTransient<IScopedService, ScopedService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Then use that middleware
app.UseFactoryActivatedMiddleware();

app.Run();
