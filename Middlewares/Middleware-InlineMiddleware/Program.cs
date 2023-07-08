using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) => {
    var cultureQuery = context.Request.Query["culture"];
    if(!string.IsNullOrEmpty(cultureQuery))
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureQuery);
    }
    await next.Invoke();
});

app.Run(async (context) =>
{
    context.Response.StatusCode = StatusCodes.Status200OK;
    await context.Response.WriteAsync("The current culture is " + CultureInfo.CurrentCulture.Name);
});

app.Run();
