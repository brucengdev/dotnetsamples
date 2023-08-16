namespace IHostedServiceExample;

public class MyHostedService: IHostedService
{
    private CancellationTokenSource _cancellationTokenSource;
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource = new();
        var token = _cancellationTokenSource.Token;
        //start a new background thread that prints out messages on the console
        ThreadPool.QueueUserWorkItem(async (object StateInfo) =>
        {
            var count = 0;
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Ping #{0}", count);
                await Task.Delay(1000, token);
                count++;
            }
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        //This is called when the application shuts down
        Console.WriteLine("Stopping MyHostedService...");
        _cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }
}