var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Please POST to /");

app.MapPost("/", async (HttpContext context) =>
{
    await context.Request.Body.CopyToAsync(context.Response.Body);
});

app.Run();