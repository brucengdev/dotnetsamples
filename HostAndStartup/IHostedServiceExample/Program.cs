using System.Numerics;
using IHostedServiceExample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MyHostedService>();

var app = builder.Build();

//List IHostedService
//For this, we have a web host
app.MapGet("/", () => $"{
    string.Join("\n", app.Services.GetServices<IHostedService>().Select((s, index) => $"Service #{index}: {s.ToString()}"))
}\n\nPlease check console for printed messages from MyHostedService");

app.Run();