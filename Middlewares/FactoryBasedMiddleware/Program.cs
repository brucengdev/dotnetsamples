using FactoryBasedMiddleware;

var builder = WebApplication.CreateBuilder(args);

//you must register the Factory based middleware as a service
builder.Services.AddTransient<FactoryActivatedMiddleware>();
builder.Services.AddTransient<IScopedService, ScopedService>();

//implement your own custom middleware factory here
//most of the time you don't need to do this, 
//the default implementation provided by .NET is used
//builder.Services.AddScoped<IMiddlewareFactory, CustomMiddlewareFactory>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Then use that middleware
app.UseFactoryActivatedMiddleware();
//app.UseConventionalMiddleware();

app.Run();
