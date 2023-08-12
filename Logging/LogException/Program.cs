var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

try {
    var a = 0;
    var test = 10 / a;
}catch(Exception ex) {
    app.Logger.LogWarning(ex, "Exception {Name} occur", nameof(Exception));
}

app.Run();
