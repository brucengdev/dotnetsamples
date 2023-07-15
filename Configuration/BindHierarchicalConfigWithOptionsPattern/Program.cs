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
//options will not be updated if the appsettings.json file is changed while the application is running
//use IOptionsSnapshot to get up-to-date configs from appsettings.json
app.MapGet("/", async (HttpContext context, IOptions<PositionOptions> positionOptions) =>
{
    var position = positionOptions.Value;
    var responseString = $"Title: {position.Title}\nName: {position.Name}";
    await context.Response.WriteAsync(responseString);
});

//this time you bind the options on the fly to an object by retrieving it from IConfiguration
app.MapGet("/bindoptions", async (HttpContext context, IConfiguration config) =>
{
    //use Bind, so it will copy data to an existing object
    var position = new PositionOptions();
    config.GetSection(PositionOptions.ConfigSectionName).Bind(position);

    //Use Get<TOptions> so it will create and return an object
    var position2 = config.GetSection(PositionOptions.ConfigSectionName).Get<PositionOptions>();

    var responseString = $"Title: {position.Title}\nName: {position.Name}";
    responseString += $"\n\nFrom .Get<T>:\nTitle: {position2?.Title}\nName: {position2?.Name}";
    await context.Response.WriteAsync(responseString);
});

//IOptionsSnapshot can get up-to-date config if the appsettings.json file is modified
//while the application is running
app.MapGet("/ioptionssnapshot", async (HttpContext context, IOptionsSnapshot<PositionOptions> positionOptions) =>
{
    var position = positionOptions.Value;
    var responseString = $"Title: {position.Title}\nName: {position.Name}";
    await context.Response.WriteAsync(responseString);
});

app.Run();
