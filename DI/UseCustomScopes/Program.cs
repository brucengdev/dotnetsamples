/***
 * Custom scopes are used in unit tests and integration tests
 * You can use custom scopes to replace dependencies with mocked ones
 * Or change settings to different values for testing different scenarios
 *
 * Actually it's not really a scope, it just replaces some data in its constructor
 * And then revert it back when it is disposed, it has nothing to do with the DI scope
 */

using Microsoft.Extensions.Options;
using UseCustomScopes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SettingsOptions>(
  builder.Configuration.GetSection("Settings")
);

var app = builder.Build();

app.MapGet("/", () => "call /default to get default settings\ncall /scoped to get scoped settings");

static string BuildSettingListing(SettingsOptions settings)
{
    return $"SettingA = {settings.SettingA}\nSettingB = {settings.SettingB}\nSettingC = {settings.SettingC}";
}

app.MapGet("/default", async (HttpContext context, IOptions<SettingsOptions> settingsOptions) =>
{
    await context.Response.WriteAsync(BuildSettingListing(settingsOptions.Value));
});

app.MapGet("/scoped", async (HttpContext context, IServiceProvider serviceProvider) =>
{
    //This SettingsScope is not actually a DI scope
    //It just replaces the settings in its constructor
    //And then restore the settings to previous values when it is disposed
    
    //It is not related to DI scopes at all
    using (new SettingsScope(serviceProvider, (settings) =>
           {
               settings.SettingA = 3;
               settings.SettingB = 3;
               settings.SettingC = "bar";
           }))
    {
        await context.Response.WriteAsync(BuildSettingListing(serviceProvider.GetRequiredService<IOptions<SettingsOptions>>().Value));
    }
});

app.Run();