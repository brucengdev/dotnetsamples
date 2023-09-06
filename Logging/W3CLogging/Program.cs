using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

//configure W3C logging options here
builder.Services.AddW3CLogging(logging =>
{
    // Log all W3C fields
    logging.LoggingFields = W3CLoggingFields.All;

    logging.AdditionalRequestHeaders.Add("x-forwarded-for");
    logging.AdditionalRequestHeaders.Add("x-client-ssl-protocol");
    logging.FileSizeLimit = 5 * 1024 * 1024;
    logging.RetainedFileCountLimit = 2;
    logging.FileName = "MyLogFile";
    logging.LogDirectory = @"logs";
    logging.FlushInterval = TimeSpan.FromSeconds(2);
});

var app = builder.Build();

//enable W3CLogging middleware, which will logs infos on HTTP requests and responses
app.UseW3CLogging();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("MyResponseHeader", "MyResponseHeader value");
    
    //will be redacted due to config
    context.Response.Headers.Add("MyResponseHeader2", "MyResponseHeader value");
    await next();
});

app.MapGet("/", () => @"
    The quick brown fox jumps over the lazy dog
    The quick brown fox jumps over the lazy dog
    The quick brown fox jumps over the lazy dog
    The quick brown fox jumps over the lazy dog
    The quick brown fox jumps over the lazy dog
    The quick brown fox jumps over the lazy dog
    The quick brown fox jumps over the lazy dog
");

app.Run();