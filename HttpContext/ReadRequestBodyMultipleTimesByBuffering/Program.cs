var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (HttpContext context, Func<Task> next) =>
{
    //request body can only be read once by forward only reading, this avoid having to buffer the request body
    //you can enable Buffering by calling EnableBuffering, so that the request body can be read multiple times
    //and rewind by different middlewares.
    //EnableBuffering must be called before any reading is done
    context.Request.EnableBuffering();
    var firstTimeRead = "";
    var secondTimeRead = "";
    using (var reader = new StreamReader(context.Request.Body))
    {
        firstTimeRead = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;//rewind to read again
        secondTimeRead = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0; //rewind for next middlewares to process
    }

    await context.Response.WriteAsync("First time: " + firstTimeRead + "\n Second time: " + secondTimeRead);
    //if you starts writing response, don't invoke the next middleware because it will throw an error
    //when trying to add information to the response
    //only writes to response if your middleware is the terminal middleware
    //await next.Invoke();
});
//
// app.Use(async (context, next) =>
// {
//     //the next middleware can read the body again
//     using (var reader = new StreamReader(context.Request.Body))
//     {
//         var requestBody = await reader.ReadToEndAsync();
//     }
//     await next.Invoke();
// });

app.Run();