namespace FactoryBasedMiddleware;

public class ConventionalMiddleware {
    private readonly RequestDelegate _next;
    public ConventionalMiddleware(RequestDelegate next) {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, IScopedService scopedService)
    {
        await context.Response.WriteAsync("From conventional middleware, message = " + scopedService.GetMessage());
    }
}