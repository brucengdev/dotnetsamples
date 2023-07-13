var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//depending on the current environment, the value for StudentName will be different
app.MapGet("/", (HttpContext context, IConfiguration config) =>
    string.Format("CommonConfig: {0}\nStudentName:{1}",
        config["CommonConfig"], config["StudentName"])
);

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var config = services.GetRequiredService<IConfiguration>();
    Console.WriteLine("CommonConfig: {0}\nStudentName:{1}", 
        config["CommonConfig"],
        config["StudentName"]);
}

app.Run();
