var builder = WebApplication.CreateBuilder(args);

//all configs come from files in the folder
//the filenames are keys while the file contents are values
var configFolderPath = Path.GetFullPath("./ConfigFolder/");
builder.Configuration.AddKeyPerFile(configFolderPath, optional: false);

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    var responseStr = $"Logging:LogLevel:System={config["Logging:LogLevel:System"]}\nStudent:Name={config["Student:Name"]}";
    await context.Response.WriteAsync(responseStr);
});

app.Run();