using CustomLogger;

var builder = WebApplication.CreateBuilder(args);

//configure the custom logging providers
builder.Logging.ClearProviders();
builder.Logging.AddColorConsoleLogger(configuration =>
{
    configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
    configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogDebug(1, "debug log");
logger.LogInformation(2, "information log");
logger.LogWarning(3, "Warning");
logger.LogError(4, "An error");
logger.LogTrace(5, "This is a trace message");

app.Run();