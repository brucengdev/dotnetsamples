using Microsoft.Extensions.Options;
using SecretManagerDemo;
using System.Data.SqlClient;

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
    var title = config["Movies:Title"];
    response += $"\nMovies:Title = {title}";

    //var connString = config["ConnectionStrings:MyDatabase"];
    var connString = config.GetConnectionString("MyDatabase");
    var builder = new SqlConnectionStringBuilder(connString);
    builder.Password = config["Password"]??"";//Password is retrieved through secret.
    connString = builder.ConnectionString;

    response += $"\nConnectionStrings:MyDatabase = {connString}";
    await context.Response.WriteAsync(response);
});

//Retrieve secrets through DI
app.MapGet("/bindsecrets", async (HttpContext context, IOptions<MoviesOptions> moviesOptions) => {
    var response = $"Please run create_secret.sh if you haven't done so";
    var title = moviesOptions.Value.Title;
    response += $"\nMovies:Title = {title}";
    await context.Response.WriteAsync(response);
});

app.Run();
