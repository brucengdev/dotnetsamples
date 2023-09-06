using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

//configure http logging options here
builder.Services.AddHttpLogging(logging =>
{
    //Fields are things like: RequestHeaders, ResponseHeaders,etc.
    logging.LoggingFields = HttpLoggingFields.All;
    
    //can also use bitwise OR to select the fields to log
    //logging.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.RequestMethod;
    
    //The following configs determine which request headers or response headers will be logged.
    //similar with media type.
    //if the name is not specified, such headers will be shown with the value being REDACTED
    //to avoid leaking sensitive information in logs
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    
    //for application/javascript media type, use text as the encoding.
    //MediaTypeOptions is used to specify which encoding to use for logging a media type
    logging.MediaTypeOptions.AddText("application/javascript");
    //use MediaTypeOptions to log types that are not logged by default
    //like application/x-www-form-urlencoded or multipart/form-data
    
    //limit the request body in log messages
    logging.RequestBodyLogLimit = 100;
    logging.ResponseBodyLogLimit = 22;
});

var app = builder.Build();

//enable HttpLogging middleware, which will logs infos on HTTP requests and responses
//Make sure the log level for Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware is set to Information 
app.UseHttpLogging();

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