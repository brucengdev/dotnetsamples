using System.Numerics;
using IHostedServiceExample;

//If you use WebApplication.CreateBuilder, it already creates a generic host with a IHostedService implementation
//for the web server
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureHostOptions(hostOptions =>
{
    //the default ShutdownTimeout is 30 seconds, to wait for all background tasks to finish
    //after that, it is no longer a grace shutdown
    hostOptions.ShutdownTimeout = TimeSpan.FromSeconds(60);
});

//Register your own IHostedService
builder.Services.AddHostedService<MyHostedService>();

var app = builder.Build();

//List IHostedService
//For this, we have a web host
app.MapGet("/", () => $"{
    string.Join("\n", app.Services.GetServices<IHostedService>().Select((s, index) => $"Service #{index}: {s.ToString()}"))
}\n\nPlease check console for printed messages from MyHostedService");

app.Run();