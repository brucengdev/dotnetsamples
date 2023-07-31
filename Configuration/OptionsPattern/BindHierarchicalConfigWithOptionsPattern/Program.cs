using BindHierarchicalConfigWithOptionsPattern;
using Microsoft.Extensions.Options;

/*
 * Basically
 * 1. Use IOptions if options are loaded at the start of the application and never reload.
 * 2. Use IOptionsSnapshot to make sure options are up-to-date for every request.
 * 3. Use IOptionsMonitor to get up-to-date options for singletons.
 * The first approach is most performant, but options are not reloaded in real time when config files
 * are changed
 */

var builder = WebApplication.CreateBuilder(args);

//Use Services.Configure<TOptions> to create a Configuration object and inject it to the DI container
builder.Services.Configure<PositionOptions>(
    builder.Configuration.GetSection(PositionOptions.ConfigSectionName)
);

builder.Services.AddSingleton<ISingletonService, SingletonService>();

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
//IOptionsSnapshot is a scoped service, so it recomputes the options for every request.
//It can affect performance of the app, so be careful here
app.MapGet("/ioptionssnapshot", async (HttpContext context, IOptionsSnapshot<PositionOptions> positionOptions) =>
{
    var position = positionOptions.Value;
    var responseString = $"Title: {position.Title}\nName: {position.Name}";
    await context.Response.WriteAsync(responseString);
});

//IOptionsMonitor get the values of the options at any given time
//suitable for singletons, this example inject a singleton that uses IOptionsMonitor to fetch options
//Try updating the appsettings.json file configs and see if the endpoint returns new values
app.MapGet("/ioptionsmonitor", async (HttpContext context, ISingletonService service) =>
{
    var responseString = $"Title: {service.Title}\nName: {service.Name}";
    await context.Response.WriteAsync(responseString);
});

app.Run();
