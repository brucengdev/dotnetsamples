using Microsoft.Extensions.Options;
using SecretManagerDemo;

var builder = WebApplication.CreateBuilder(args);

//You can register secrets as a POCO in DI
builder.Services.Configure<MoviesOptions>(
    builder.Configuration.GetSection("Movies")
);
var app = builder.Build();

//you can get secrets from secret manager just like other configuration
//the secret config provider is registered by default with WebApplication.CreateBuilder
app.MapGet("/", async (HttpContext context, IConfiguration config) => {
    var response = $"Please run create_secret.sh if you haven't done so";
    var secret = config["Movies:ConnectionString"];
    response += $"\nThe secret is {secret}";
    await context.Response.WriteAsync(response);
});

//Retrieve secrets through DI
app.MapGet("/bindsecrets", async (HttpContext context, IOptions<MoviesOptions> moviesOptions) => {
    var response = $"Please run create_secret.sh if you haven't done so";
    var secret = moviesOptions.Value.ConnectionString;
    response += $"\nThe secret is {secret}";
    await context.Response.WriteAsync(response);
});

app.Run();
