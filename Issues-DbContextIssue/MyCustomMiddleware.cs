using Issues_DbContextIssue;

public class MyCustomMiddleware
{
    private RequestDelegate _next;

    public MyCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITransientService transientService, IScopedService scopedService)
    {
        await transientService.DoStuff();
        await context.Response.WriteAsync("Executed");
        await scopedService.DoStuff();
        //await _next.Invoke(context);
    }
}

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<MyCustomMiddleware>();
        return app;
    }
}
