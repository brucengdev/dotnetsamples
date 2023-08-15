using System.Text;
using HostAndStartup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;

namespace HostBuilder;

public class Program {
    public static void Main(string[] args) {
        // WithGenericHost(args);
        //WithWebHostAndStartup(args);
        //WithWebApplication(args);
        WithWebApplicationBuilder(args);
    }

    private static void WithGenericHost(string[] args) {
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(builder => {
            builder.ConfigureServices((services) => {
                //register services to DI here
            });
            builder.Configure((appBuilder) => {
                appBuilder.Use(async (HttpContext context, RequestDelegate next) => {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Hello, world!"));
                });
            });
        }).Build()
        .Run();
    }

    private static void GenericHostWithStartup(string[] args) {
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(builder => {
            builder.UseStartup<Startup>();
        }).Build()//build a host
        .Run();//run the host
    }

    private static void WithWebHostAndStartup(string[] args)
    {
        //WebHost is deprecated and used mostly for compatibility
        var builder = WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
        builder.Build().Run();
    }

    private static void WithWebApplication(string[] args) {
        var app = WebApplication.Create(args);//create and build a webapp
        app.Use(async (HttpContext context, RequestDelegate next) => {
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Hello, World!"));
        });
        app.Run();
    }

    private static void WithWebApplicationBuilder(string[] args)
    {
        var appBuilder = WebApplication.CreateBuilder(args);
        //register services here
        appBuilder.Services.AddSingleton<Dependency>();

        //build an app
        var app = appBuilder.Build();

        //then configure middlewares
        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Hello, World!"));
        });
        app.Run();
    }
}