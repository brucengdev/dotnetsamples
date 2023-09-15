namespace LivenessAndReadiness;

public class BackgroundTaskService: BackgroundService
{
    private readonly MyHealthCheck _myHealthCheck;
    public BackgroundTaskService(MyHealthCheck myHealthCheck)
    {
        _myHealthCheck = myHealthCheck;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(15));
        _myHealthCheck.IsReady = true;
    }
}