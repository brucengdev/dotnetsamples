var s_client = new HttpClient();

var urls = new string[] {
    "https://learn.microsoft.com",
    "https://learn.microsoft.com/aspnet/core",
    "https://learn.microsoft.com/azure",
    "https://learn.microsoft.com/azure/devops",
    "https://learn.microsoft.com/dotnet",
    "https://learn.microsoft.com/dynamics365",
    "https://learn.microsoft.com/education",
    "https://learn.microsoft.com/enterprise-mobility-security",
    "https://learn.microsoft.com/gaming",
    "https://learn.microsoft.com/graph",
    "https://learn.microsoft.com/microsoft-365",
    "https://learn.microsoft.com/office",
    "https://learn.microsoft.com/powershell",
    "https://learn.microsoft.com/sql",
    "https://learn.microsoft.com/surface",
    "https://learn.microsoft.com/system-center",
    "https://learn.microsoft.com/visualstudio",
    "https://learn.microsoft.com/windows",
    "https://learn.microsoft.com/xamarin"
};

async Task DownloadAsync(string url, CancellationToken token) {
    Console.WriteLine("Downloading {0}", url);
    var response = await s_client.GetAsync(url, token);
    var content = await response.Content.ReadAsStringAsync(token);
    Console.WriteLine("Downloaded URL {0}: {1}", url, content.Substring(0, 40).Replace("\n", ""));
}

async Task CancelAsync(CancellationTokenSource tokenSource) {
    await Task.Run(() => {
        do {
            Console.WriteLine("Press ENTER to cancel all downloads...");
        }while(Console.ReadKey().Key != ConsoleKey.Enter);
        Console.WriteLine("Enter key pressed, cancelling downloads...");
        tokenSource.Cancel();
    });
}

var tokenSource = new CancellationTokenSource();
var downloadTasks = from url in urls select DownloadAsync(url, tokenSource.Token);
var cancelTask = CancelAsync(tokenSource);

try {
    var tasks = downloadTasks.Append(cancelTask).ToList();
    await Task.WhenAll(tasks);
}catch(TaskCanceledException) {

}