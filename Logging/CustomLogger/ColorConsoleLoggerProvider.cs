using System.Collections.Concurrent;
using System.Runtime.Versioning;
using Microsoft.Extensions.Options;

namespace CustomLogger;

[UnsupportedOSPlatform("browser")]
//Configuration section can define options using ColorConsole Key
/*
 * {
    "Logging": {
        "ColorConsole": {
            "LogLevelToColorMap": {
                "Information": "DarkGreen",
                "Warning": "Cyan",
                "Error": "Red"
            }
        }
    }
}
 */
[ProviderAlias("ColorConsole")]
public class ColorConsoleLoggerProvider:ILoggerProvider
{
    private readonly IDisposable? _onChangeToken;
    private ColorConsoleLoggerConfiguration _currentConfig;
    private readonly ConcurrentDictionary<string, ColorConsoleLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public ColorConsoleLoggerProvider(IOptionsMonitor<ColorConsoleLoggerConfiguration> config)
    {
        _currentConfig = config.CurrentValue;
        _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
    }
    
    public void Dispose()
    {
        _loggers.Clear();
        _onChangeToken.Dispose();
    }

    public ColorConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new ColorConsoleLogger(name, GetCurrentConfig));
    }
}