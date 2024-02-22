var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.MapGet("/", () => "Example").RequireHost("www.example.com");//only go here if host is www.example.com
app.MapGet("/", () => "Contoso").RequireHost("www.contoso.com");//only go here if host is www.contoso.com
app.MapGet("/", () => "Localhost").RequireHost("localhost:*");//only go here if host is localhost
app.MapGet("/", () => "Default");//otherwise go to this

//See controller example on how to set require host on controller actions
app.MapControllers();

app.Run();