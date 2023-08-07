using ClearDefaultLoggingSettings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGreetingService, GreetingService>();

// Add services to the container.

/*
 * The default logging providers are
 * - Console
 * - Debug
 * - Windows Event Log (on Windows)
 * etc
 */

//clear default logging providers, and set only the Console
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

//another way to override default logging configs
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddConsole();
});

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IGreetingService greetingService) =>
{
    await context.Response.WriteAsync($"The message is {greetingService.GetGreeting()}, check the logs.");
});

app.Run();