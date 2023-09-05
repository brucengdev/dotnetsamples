var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//enable HttpLogging middleware, which will logs infos on HTTP requests and responses
//Make sure the log level for Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware is set to Information 
app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");

app.Run();