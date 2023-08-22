var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*
 * A default implicit scope is created for logs with ParentId, SpanId, TraceId, Baggage and Tags
 * These can be configured in code if there requires some customization
 */
var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.Configure(options =>
    {
        options.ActivityTrackingOptions = ActivityTrackingOptions.ParentId |
                                          ActivityTrackingOptions.SpanId |
                                          ActivityTrackingOptions.TraceId |
                                          ActivityTrackingOptions.Baggage |
                                          ActivityTrackingOptions.Tags;
    }).AddSimpleConsole(options =>
    {
        options.IncludeScopes = true;
    });
});
var logger = loggerFactory.CreateLogger("Foo");
logger.LogInformation("Test");

app.MapGet("/", () => "Hello World!");

app.Run();