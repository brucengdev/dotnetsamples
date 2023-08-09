var builder = WebApplication.CreateBuilder(args);

//First option: create a LoggerFactory, then use that to create a logger to write logs
var loggerFactory = LoggerFactory.Create(config =>
{
    config.AddConsole();
});

//Second option: use the Logger in app.Logger
var app = builder.Build();
app.Logger.LogInformation("This is an informational message from Program.cs");
app.Logger.LogWarning("This is a warning message from Program.cs");
app.Logger.LogError("This is an error message from Program.cs");

var logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("This is an informational message from logger created from logger factory");
logger.LogWarning("This is a warning message from logger created from logger factory");
logger.LogError("This is an error message from logger created from logger factory");

app.MapGet("/", () => "Check the console for log messages");
app.Run();