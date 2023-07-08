using DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//demonstrates using an extension method to register related services
builder.Services.AddRelatedServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//example of creating a scope to resolve some service during that scope
using (var serviceScope = app.Services.CreateScope())
{
    var service = serviceScope.ServiceProvider.GetRequiredService<IScopedDep>();
    Console.WriteLine(service.GetMessage());
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
