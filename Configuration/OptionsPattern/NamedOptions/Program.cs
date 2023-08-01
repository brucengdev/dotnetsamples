using NamedOptions;

/*
 * This example demonstrates how to use named options.
 * If the same options set are repeated multiple times, each with a different name, then you can use
 * IOptionsSnapshot to get the correct options.
 */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var thisMonthConfig = builder.Configuration.GetSection("TopItems.Month");
builder.Services.Configure<TopItemsSettings>("Month", thisMonthConfig);
var thisYearConfig = builder.Configuration.GetSection("TopItems.Year");
builder.Services.Configure<TopItemsSettings>("Year", thisYearConfig);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();