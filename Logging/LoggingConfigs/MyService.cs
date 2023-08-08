namespace LoggingConfigs;

public class MyService: IMyService
{
    private readonly ILogger<MyService> _logger;
    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void WriteLogs()
    {
        /*
         public enum LogLevel
        {
            Trace = 0,
            Debug = 1,
            Information = 2,
            Warning = 3,
            Error = 4,
            Critical = 5,
            None = 6,
        }
        */
        //when debugging, only errors and above will be logged
        
        _logger.LogTrace("MyService: This is a trace message");
        _logger.LogDebug("MyService: This is a debug message");
        _logger.LogInformation("MyService: This is an informational message");
        _logger.LogWarning("MyService: This is a warning message");
        _logger.LogError("MyService: This is an error message");
    }
}