using OptionsBuilderExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<ITabletRepo, TabletRepo>();

//the current best model, use a service to resolve the option
builder.Services.AddOptions<TopItemSettings>().Configure((TopItemSettings o, ITabletRepo tabletRepo) =>
{
    var bestModel = tabletRepo.GetBestModel();
    o.Name = bestModel.Name;
    o.Model = bestModel.Model;
});

//example to create a named options, use the IOptionsSnapshot to resolve by name
builder.Services.AddOptions<TopItemSettings>("Year").Configure((o) =>
{
    o.Model = "GTS8";
    o.Name = "GalaxyTab S8";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();