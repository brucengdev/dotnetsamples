using System.Text;
using BindHierarchicalConfigWithOptionsPattern;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Use Services.Configure<TOptions> to create a Configuration object and inject it to the DI container
builder.Services.Configure<PositionOptions>(
    builder.Configuration.GetSection(PositionOptions.ConfigSectionName)
);

var app = builder.Build();

//Resolve the options from DI by using IOptions<TOptions>
app.MapGet("/", async (HttpContext context, IOptions<PositionOptions> positionOptions) =>
{
    var position = positionOptions.Value;
    var responseString = $"Title: {position.Title}\nName: {position.Name}";
    await context.Response.WriteAsync(responseString);
});

//this time you bind the options on the fly to an object by retrieving it from IConfiguration
app.MapGet("/bindoptions", async (HttpContext context, IConfiguration config) =>
{
    var position = new PositionOptions();
    config.GetSection(PositionOptions.ConfigSectionName).Bind(position);
    var responseString = $"Title: {position.Title}\nName: {position.Name}";
    await context.Response.WriteAsync(responseString);
});

app.Run();
