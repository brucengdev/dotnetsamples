var builder = WebApplication.CreateBuilder(args);

/*
 * LogLevel is just like any other configuration and can be set by any config provider
 * Just follow the samples under Configuration code samples
 */

var app = builder.Build();

app.Logger.LogInformation("This is an informational message");
app.Logger.LogWarning("This is a warning message");
app.Logger.LogError("This is an error message");

app.Run();