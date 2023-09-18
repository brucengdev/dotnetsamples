var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Please POST to /");

app.MapPost("/", async (HttpContext context) =>
{
    //request body can only be read once by forward only reading, this avoid having to buffer the request body
    await context.Request.Body.CopyToAsync(context.Response.Body);
});

app.Run();