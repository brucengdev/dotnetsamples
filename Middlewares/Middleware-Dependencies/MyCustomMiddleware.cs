namespace Middleware_Dependencies
{
    public class MyCustomMiddleware
    {
        private ISingletonDependency _singletonDependency;
        private RequestDelegate _next;
        public MyCustomMiddleware(RequestDelegate next, ISingletonDependency singletonDependency) {
            _next = next;
            _singletonDependency = singletonDependency;
        }

        public async Task InvokeAsync(HttpContext context, 
            ITransientDependency _transientDependency, IScopedDependency _scopedDependency)
        {
            /**
             * Singleton dependency has application lifetime and will return the same message
             * Transient dependency is created for each time it is requested, then it is disposed
             * Scoped dependency has the lifetime of a HTTP request.
             **/
            await context.Response.WriteAsync($@"
                <p>{_singletonDependency.Message}</p>
                <p>{_transientDependency.Message}</p>
                <p>{_scopedDependency.Message}</p>                    
            ");

            //await _next.Invoke(context);
        }
           
    }
}
