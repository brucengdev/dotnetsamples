var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    var keys = new List<string>
    {
        "ConnectionStrings:MyCustomDb",
        "ConnectionStrings:MyMySqlDb",
        "ConnectionStrings:MySqlDb",
        "connectionStrings:MySQLAzureDb"
    };
    var responseStr = string.Join("\n", keys.Select(k => $"{k}={config[k]}\n{k}_ProviderName={config[k+"_ProviderName"]}"));
    responseStr += "\n\nRun connstring_prefixes.sh to set the environment variables";
    await context.Response.WriteAsync(responseStr);
});

app.Run();
