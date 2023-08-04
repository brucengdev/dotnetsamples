using OptionsValidation;

var builder = WebApplication.CreateBuilder(args);

/*
 * This example demonstrates
 * 1- Validation using data annotations
 * 2- Validation using a delegate
 *  
 */

// Add services to the container.
builder.Services.AddRazorPages();

/*
 * MyConfigOptions has annotations to set validation rules for each configuration
 */

builder.Services.AddOptions<MyConfigOptions>()
    .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig))
    //validate annotations
    .ValidateDataAnnotations() 
    //add a complex validation using a delegate
    .Validate(o =>
    {
        return o.Key2 > o.Key3;
    }, "Key2 must be greater than Key3");

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