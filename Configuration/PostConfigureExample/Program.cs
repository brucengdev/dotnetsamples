using PostConfigureExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//the current best model, use a service to resolve the option
builder.Services.AddOptions<TopItemsSettings>().Configure((o) =>
{
    o.Model = "1234";
    o.Name = "iPad Air";
    o.Description = "an apple product";
});

//example to create a named options, use the IOptionsSnapshot to resolve by name
builder.Services.AddOptions<TopItemsSettings>("Year").Configure((o) =>
{
    o.Model = "GTS8";
    o.Name = "GalaxyTab S8";
    o.Description = "a Samsung product";
});

//post configure will change options after all the Configure<TOptions> ran
builder.Services.PostConfigure<TopItemsSettings>(o =>
{
    o.Description = "an apple product (description modified by PostConfigure)";
});
builder.Services.PostConfigure<TopItemsSettings>("Year", o =>
{
    o.Description = "a Samsung product (description modified by PostConfigure)";
});
//PostConfigureAll will change all instances of the options
builder.Services.PostConfigureAll<TopItemsSettings>(o =>
{
    o.Model = "XXXX (Modified by PostConfigureAll())";
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

app.MapRazorPages();

app.Run();
