var builder = WebApplication.CreateBuilder(args);

//add a collection of key-value pairs in memory as configurations
var configs = new Dictionary<string, string>
{
    { "MyKey", "Foo" },
    { "Position:Title", "Assistant" }
};

builder.Configuration.AddInMemoryCollection(configs);

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    var responseStr = $"MyKey={config["MyKey"]}\nPosition:Title={config["Position:Title"]}";
    await context.Response.WriteAsync(responseStr);
});

app.Run();