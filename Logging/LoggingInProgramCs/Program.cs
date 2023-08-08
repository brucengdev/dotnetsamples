var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Logger.LogInformation("This is an informational message from Program.cs");
app.Logger.LogWarning("This is a warning message from Program.cs");
app.Logger.LogError("This is an error message from Program.cs");

app.MapGet("/", () => "Check the console for log messages");
app.Run();