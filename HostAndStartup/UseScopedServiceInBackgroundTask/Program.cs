using UseScopedServiceInBackgroundTask;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<IMyScopedService, MyScopedService>();
    })
    .Build();

host.Run();