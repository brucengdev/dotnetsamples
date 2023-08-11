var builder = WebApplication.CreateBuilder(args);

/*
 * Log category is just a category to organize logs, so that you know logs come from which source
 * or which category of functionality
 * ILogger<T> will use the full class name as category by default, but the category is just a string
 */

var app = builder.Build();

//using ILogger<T>, then category is the fullname of type T
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("This log message is from ILogger<Program>");

//Use a logger factory to create a logger in a custom category name
var loggerFactory = LoggerFactory.Create(loggerFactoryBuilder =>
{
  loggerFactoryBuilder.AddConsole();
});
var aLogger = loggerFactory.CreateLogger("MyCustomCategory");
aLogger.LogInformation("This log message is from category MyCustomCategory");

app.Run();