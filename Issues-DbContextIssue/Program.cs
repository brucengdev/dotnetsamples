using Issues_DbContextIssue;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EFTestDbContext>(optionsBuilder =>
{
    var connString = builder.Configuration.GetConnectionString("EFTestDbContext");
    optionsBuilder.UseSqlServer(connString);
});

builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddScoped<IScopedService, ScopedService>();

var app = builder.Build();
app.UseMyCustomMiddleware();
app.Run();
