namespace UseScopedServiceInBackgroundTask;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //to use a scoped service in a background task, we need to create a scope
        using (var scope = _serviceProvider.CreateScope())
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IMyScopedService>();
                var msg = scopedService.GetMessage();
                Console.WriteLine("The message from scoped service is {0}", msg);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}