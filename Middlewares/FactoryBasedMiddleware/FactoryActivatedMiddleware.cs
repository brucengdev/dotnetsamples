using System.Text;

namespace FactoryBasedMiddleware;

public class FactoryActivatedMiddleware: IMiddleware
{
    private readonly IScopedService _scopedService;
    public FactoryActivatedMiddleware(IScopedService scopedService)
    {
        _scopedService = scopedService;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("From factory activated middleware, message: " +
                                               _scopedService.GetMessage());
    }
}