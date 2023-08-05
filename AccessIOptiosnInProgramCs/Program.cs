using AccessIOptiosnInProgramCs;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<TopItemsSettings>().Configure((o) =>
{
    o.Model = "1234";
    o.Name = "iPad Air";
    o.Description = "an apple product";
});

var app = builder.Build();

var topItems = app.Services.GetRequiredService<IOptions<TopItemsSettings>>();
Console.WriteLine("This model is {0} - {1} ({2})", topItems.Value.Name, topItems.Value.Model, topItems.Value.Description);

app.Run();
