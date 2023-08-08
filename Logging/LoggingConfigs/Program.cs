using LoggingConfigs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMyService, MyService>();

var app = builder.Build();

//when running in release mode, only infos and higher are logged, aspnetcore warnings and higher are logged.
//when running in development mode, only warnings and higher are logged to console, MyService only logs errors and higher

app.MapGet("/", async (HttpContext context, IMyService service, ILogger<string> someOtherLogger) =>
{
    //for ILogger<string>, expect that only the warning message is logged
    someOtherLogger.LogInformation("string: this is an informational message");
    someOtherLogger.LogWarning("string: this is a warning message");
    service.WriteLogs();
    await context.Response.WriteAsync("Please check console for log messages");
});

app.Run();