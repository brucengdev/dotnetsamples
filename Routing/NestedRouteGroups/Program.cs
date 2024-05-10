var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

var all = app.MapGroup("");
//with groups, you can apply settings to all endpoints in a group
//then you can have nested groups with different configurations
var org = all.MapGroup("{org}").RequireAuthorization();
var user = org.MapGroup("{user}");
user.MapGet("", (string org, string user) => $"{org}/{user}");

app.Run();