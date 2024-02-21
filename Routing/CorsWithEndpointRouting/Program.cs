var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var corsPolicyName = "_MyAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins("http://example.com");
    });
});

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/withoutcors", () => "You can access this from any origin");
    
    //try to make an ajax request to this to see cors error
    endpoints.MapGet("/withcors", () => "You can only access it from allowed origin")
        .RequireCors(corsPolicyName);
});

app.Run();