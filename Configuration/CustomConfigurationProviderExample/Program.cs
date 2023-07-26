using CustomConfigurationProviderExample.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEFConfiguration(
    opt => opt.UseInMemoryDatabase("InMemoryDb"));

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    var keys = new[] { "quote1", "quote2", "quote3" };
    var responseStr = string.Join("\n", keys.Select(k => $"{k}={config[k]}"));
    await context.Response.WriteAsync(responseStr);
});

app.Run();