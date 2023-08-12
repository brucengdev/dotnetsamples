var builder = WebApplication.CreateBuilder(args);

/*
 * Generally, log filtering should be done through configuration
 * and not code.
 * This should only be done when the configurations are removed somehow
 * Like missing appsettings.json.
 * So basically, NEVER use this, but should know what this is
 */

builder.Logging.AddFilter((string provider, string category, LogLevel level) =>
{
    //only write log message for console provider and log level from Warning and above.
    return provider.Contains("ConsoleLoggerProvider")
        && level >= LogLevel.Warning;
});

var app = builder.Build();

app.Logger.LogInformation("This is a log message of information");
app.Logger.LogWarning("This is a warning log message");
app.Logger.LogError("This is an error log message");

app.Run();
