using System.Net;

var listener = new HttpListener();
listener.Prefixes.Add("https://*:8000/");
listener.Start();
while (true)
{
    var context = listener.GetContext();
    var request = context.Request;

    context.Response.StatusCode = 200;
    context.Response.OutputStream.Write(Array.Empty<byte>(), 0, 0);
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Close();
}