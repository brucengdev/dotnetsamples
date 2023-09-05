
static async Task<string> FetchFromUrl(string url)
{
    var message = "";
    using (var client = new HttpClient())
    {
        message = await client.GetStringAsync(url);
    }

    return message;
}

//This seems to be no longer supported in .NET Core.
//See https://devblogs.microsoft.com/dotnet/migrating-delegate-begininvoke-calls-for-net-core/
GetMessage myDelegate = FetchFromUrl;

//Calling BeginInvoke starts executing the delegate on a separate threadpool thread
var asyncResult = myDelegate.BeginInvoke("https://vnexpress.net", null, null);

//EndInvoke waits for the delegate to finish and return the result
var result = myDelegate.EndInvoke(asyncResult).Result;

Console.WriteLine(result);

delegate Task<string> GetMessage(string url);