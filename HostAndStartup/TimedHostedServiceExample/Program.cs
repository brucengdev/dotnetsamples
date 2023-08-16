using TimedHostedServiceExample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MyTimedHostedService>();

var app = builder.Build();

app.Run();