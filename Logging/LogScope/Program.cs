var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

/*
 * Using log scopes allows you to add common data to related log messages to group them together
 * For example, in multi layer architecture, same request can create 4-5 log messages from each layer.
 * You may want to group them together using a transaction ID or request timestamp/ID.
 */

using (app.Logger.BeginScope(new Dictionary<string, string>
       {
              { "Product", "iPad" },
              { "Model", "Gen 8" }
       }))
{
       app.Logger.LogInformation("This is a tablet");
       app.Logger.LogInformation("Using log scope can attach data to multiple log messages that are related");
       //but then how do you view these data?
       //I guess it depends on the log providers, you probably can't see the values in console
       //but for some other log providers it's possible to view all the log scope variables
}

app.MapGet("/", () => "Please check the console");

app.Run();