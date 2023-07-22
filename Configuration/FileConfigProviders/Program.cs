var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddIniFile("MyIniConfig.ini", optional: true, reloadOnChange: true)
    .AddIniFile($"MyIniConfig.{builder.Environment.EnvironmentName}.ini", optional: true, reloadOnChange: true);


var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    var responseStr = $"MyIniConfig1={config["MyIniConfig1"]}\nMyIniConfig2={config["MyIniConfig2"]}";
    responseStr += $"\nPosition:Title={config["Position:Title"]}\nLogging:LogLevel:Microsoft={config["Logging:LogLevel:Microsoft"]}";
    await context.Response.WriteAsync(responseStr);
});

app.Run();
