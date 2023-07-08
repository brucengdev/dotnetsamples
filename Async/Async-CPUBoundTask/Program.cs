async Task GetWebpageAsync() {
    Console.WriteLine("Example of IO bound task: retrieving a web page");
    string content = "";
    using (var client = new HttpClient())
    {
        var response = await client.GetAsync("https://vnexpress.net");
        content = await response.Content.ReadAsStringAsync();
    }
    Console.WriteLine(content.Substring(0, 80) + "...");
}

long CalculateNthFibonacciNumber(int n)
{
    if(n == 0) return 0;
    if(n == 1) return 1;
    if(n == 2) return 1;
    long last = 1;
    long secondLast = 1;
    long value = 0;
    for(int i = 3; i <= n; i++)
    {
        value = last + secondLast;
        secondLast = last;
        last = value;
        Thread.Sleep(10);//make it take a bit longer
    }
    return value;
}

async Task CalculateNthFibonacciNumberAsync(int n)
{
    Console.WriteLine("Start calculating Fibonacci number #{0}", n);
    var value = await Task.Run(() => CalculateNthFibonacciNumber(n));
    Console.WriteLine("The #{0} Fibonacci number is {1}", n, value);
}

async Task DoTasksSynchronouslyAsync()
{
    Console.WriteLine("Example of doing two async tasks synchronously");
    await GetWebpageAsync();
    await CalculateNthFibonacciNumberAsync(500);
}

async Task DoTasksAsynchronouslyAsync()
{
    Console.WriteLine("Example of doing two async tasks in non blocking way");
    var tasks = new List<Task> { GetWebpageAsync(), CalculateNthFibonacciNumberAsync(500) };
    Console.WriteLine("I can do other things while both the IO and the CPU bound tasks are being worked on");
    Thread.Sleep(2000);//simulate doing something in this thread
    await Task.WhenAll(tasks);
}

await DoTasksSynchronouslyAsync();
Console.WriteLine();
await DoTasksAsynchronouslyAsync();