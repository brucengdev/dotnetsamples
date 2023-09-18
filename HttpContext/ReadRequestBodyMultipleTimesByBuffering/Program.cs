var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Please POST to /");

app.Use(async (context, next) =>
{
    //request body can only be read once by forward only reading, this avoid having to buffer the request body
    //you can enable Buffering by calling EnableBuffering, so that the request body can be read multiple times
    //and rewind by different middlewares.
    //EnableBuffering must be called before any reading is done
    context.Request.EnableBuffering();
    var firstTimeRead = "";
    using (var reader = new StreamReader(context.Request.Body))
    {
        firstTimeRead = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;//rewind to read again
    }

    var secondTimeRead = "";
    using (var reader = new StreamReader(context.Request.Body))
    {
        secondTimeRead = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;//rewind for next middlewares to process
    }

    await context.Response.WriteAsync("First time: " + firstTimeRead + "\n Second time: " + secondTimeRead);
    await next.Invoke();
});

app.Run();