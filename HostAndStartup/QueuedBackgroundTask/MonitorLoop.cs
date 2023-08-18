
namespace QueuedBackgroundTask;

public class MonitorLoop
{
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly ILogger<MonitorLoop> _logger;
    private readonly CancellationToken _cancellationToken;

    public MonitorLoop(IBackgroundTaskQueue taskQueue,
        ILogger<MonitorLoop> logger,
        IHostApplicationLifetime applicationLifetime)
    {
        _taskQueue = taskQueue;
        _logger = logger;
        _cancellationToken = applicationLifetime.ApplicationStopping;
    }

    public void StartMonitorLoop()
    {
        _logger.LogInformation("Monitor loop started");

        Task.Run(async () => await MonitorAsync(_cancellationToken));
    }

    private async Task MonitorAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var key = Console.ReadKey();
            if (key.KeyChar != 'w')
            {
                continue; 
            }

            await _taskQueue.QueueBackgroundWorkItemAsync(BuildWorkItem);
        }
    }

    private async ValueTask BuildWorkItem(CancellationToken stoppingToken)
    {
        var taskId = Guid.NewGuid().ToString();
        _logger.LogInformation("Background task {taskid} started", taskId);
        await Task.Delay(3000, stoppingToken);
        _logger.LogInformation("Doing work #1 for {taskid}", taskId);
        await Task.Delay(3000, stoppingToken);
        _logger.LogInformation("Doing work #2 for {taskid}", taskId);
    }
}