using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*
 * You can't write to the request body and modify it, but you can replace it with an alternative mem stream
 * this is how you did it in Yokogawa
 */

app.Use(async (HttpContext context, Func<Task> next) =>
{
    var content = "";
    using (var reader = new StreamReader(context.Request.Body))
    {
        content = await reader.ReadToEndAsync();
    }

    //change all character a to b
    content = content.Replace('a', 'b');
    
    using (var memStream = new MemoryStream())
    {
        await memStream.WriteAsync(Encoding.UTF8.GetBytes(content));

        //replace the body stream
        var backupBodyStream = context.Request.Body;
        context.Request.Body = memStream;
        memStream.Position = 0;

        await next.Invoke();
        
        //restore the request body stream after next middlewares have finished working with it
        context.Request.Body = backupBodyStream;
    }//dispose the memstream
});

app.MapGet("/", () => "Please Post to /, all a character will be replaced with b");

app.MapPost("/", async (HttpContext context) =>
{
    var content = "";
    using (var reader = new StreamReader(context.Request.Body))
    {
        content = await reader.ReadToEndAsync();
    }

    return content;
});

app.Run();