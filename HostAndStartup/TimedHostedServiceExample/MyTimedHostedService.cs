namespace TimedHostedServiceExample;

public class MyTimedHostedService: IHostedService, IDisposable
{
    private int counter = 0;
    private Timer _timer;
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        Interlocked.Increment(ref counter);
        Console.WriteLine("This is a log message #{0} from a timed hosted service", counter);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping the timer...");
        //stop the timer
        _timer.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Console.WriteLine("Disposing the timer...");
        _timer.Dispose();
    }
}