using System.Text;

namespace HostBuilder;
public class Startup {
    public void ConfigureServices(IServiceCollection services) {
    }
    public void Configure(IApplicationBuilder builder, IWebHostEnvironment env) {
        builder.Use(async (HttpContext context, RequestDelegate next) => {
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Hello, world!"));
        });
    }
}