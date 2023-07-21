var builder = WebApplication.CreateBuilder(args);

var switchMappings = new Dictionary<string, string>
{
    { "-k1", "key1" },
    { "-k2", "key2" },
    { "-k3", "key3" },
    { "-alt4", "key4" }
};
builder.Configuration.AddCommandLine(args, switchMappings);

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    var responseStr = $@"
key1(-k1): {config["key1"]}
key2(-k2): {config["key2"]}
key3(-k3): {config["key3"]}
key4(-alt4): {config["key4"]}
    ".Trim();
    await context.Response.WriteAsync(responseStr);
});
app.Run();