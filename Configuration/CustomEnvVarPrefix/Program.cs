var builder = WebApplication.CreateBuilder(args);

//variables with MyCustomPrefix_ will override other configs given by previous config providers
builder.Configuration.AddEnvironmentVariables(prefix: "MyCustomPrefix_");

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    //Environment variables with prefix MyCustomPrefix_ will override
    //other configs with the same names (without the prefix)
    var value = config["MyConfig"];
    await context.Response.WriteAsync($"Configuration value is {value}");
});

app.Run();