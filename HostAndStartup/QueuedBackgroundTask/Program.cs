using QueuedBackgroundTask;

/*
 * An example of a hosted service executing tasks from a queue
 * - The hosted service runs and keeps checking the queue for tasks to execute
 * - A monitor loop is run and collect key press from user, pressing w will queue a task
 * - The hosted service dequeue the task, executes it, then wait on the queue again
 */

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<QueueHostedService>();
        services.AddSingleton<MonitorLoop>();
        services.AddSingleton<IBackgroundTaskQueue>(services =>
        {
            return new BackgroundTaskQueue(100);
        });
    })
    .Build();

var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
monitorLoop.StartMonitorLoop();
Console.WriteLine("Press W to queue a new task");
host.Run();