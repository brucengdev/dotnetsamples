using System.Buffers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

async Task<List<string>> GetLines(Stream requestBody)
{
    var buffer = ArrayPool<byte>.Shared.Rent(4096);
    var builder = new StringBuilder();
    while (true)
    {
        var readBytes = await requestBody.ReadAsync(buffer, offset: 0, buffer.Length);
        if (readBytes == 0)
        {
            break;
        }
        var stringData = Encoding.UTF8.GetString(buffer, 0, readBytes);
        builder.Append(stringData);
    }
    ArrayPool<byte>.Shared.Return(buffer);
    var requestBodyStr = builder.ToString();
    return requestBodyStr.Split('\n').ToList();
}

app.MapPost("/", async (HttpContext context) =>
{
    var requestBody = context.Request.Body;
    var lines = await GetLines(requestBody);
    await context.Response.WriteAsync(string.Join("<br />", lines));
});

app.MapGet("/", () => "Use Postman and post some text to this endpoint.");

app.Run();
