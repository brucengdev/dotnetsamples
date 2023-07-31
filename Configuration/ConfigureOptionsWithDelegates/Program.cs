using ConfigureOptionsWithDelegates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<StudentOptions>(myOptions =>
{
    myOptions.Title = "Junior";
    myOptions.Name = "Jenny";
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();