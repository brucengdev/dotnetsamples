using System.Globalization;

namespace Middleware_ClassCustomMiddleware
{
    public class RequestCultureMiddleware
    {
        private RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if(!string.IsNullOrEmpty(cultureQuery))
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureQuery);
            }
            await _next.Invoke(context);
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestCultureMiddleware>();
            return app;
        }
    }
}
