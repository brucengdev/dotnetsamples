namespace ClearDefaultLoggingSettings;

public class GreetingService: IGreetingService
{
    private readonly ILogger<GreetingService> _logger;
    public GreetingService(ILogger<GreetingService> logger)
    {
        _logger = logger;
    }
    public string GetGreeting()
    {
        _logger.LogInformation("[Log message] GetGreeting was called.");
        _logger.LogInformation("[Log message] The log will only be logged to console because that's" 
                               + " the only logging provider");
        return "Hello, world!";
    }
}