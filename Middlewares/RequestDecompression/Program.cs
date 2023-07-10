using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRequestDecompression();

var app = builder.Build();

/*** 
The RequestDecompression middleware checks the Content-Encoding Http request header
to automatically identify and decompress the request based on the algorithm
Available compressions
- br
- deflate
- gzip

Compress the data you want to test into a file (.tar.gzip for example)
Then use Postman and select the file as the request body to test this middleware
**/

app.UseRequestDecompression();

app.MapGet("/", () => "Hello World!");

app.MapPost("/", async (HttpContext context) => {
    var buffer = new byte[4096];
    var readBytes = await context.Request.Body.ReadAsync(buffer, 0, 4096);
    var value = Encoding.UTF8.GetString(buffer, 0, readBytes);
    await context.Response.WriteAsync(value);
});

app.Run();
