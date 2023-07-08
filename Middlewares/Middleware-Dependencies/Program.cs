using Middleware_Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISingletonDependency, SingletonDependency>();
builder.Services.AddTransient<ITransientDependency, TransientDependency>();
builder.Services.AddScoped<IScopedDependency, ScopedDependency>();

var app = builder.Build();

app.UseMiddleware<MyCustomMiddleware>();

app.Run();
