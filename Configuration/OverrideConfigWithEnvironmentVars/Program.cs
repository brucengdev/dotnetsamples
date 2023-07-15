var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) => {
    //the configuration value can be overwritten by the environment
    //variable ConnectionStrings__MyAppDatabase
    //note that : is replaced by __ (double underscore)
    var connString = config["ConnectionStrings:MyAppDatabase"];
    await context.Response.WriteAsync(connString??"No value found");
});
app.Run();
