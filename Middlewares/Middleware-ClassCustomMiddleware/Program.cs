using Middleware_ClassCustomMiddleware;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseMiddleware<RequestCultureMiddleware>();
//can also use extension method
app.UseRequestCulture();

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Current culture is {CultureInfo.CurrentCulture.Name}");
});

app.Run();
