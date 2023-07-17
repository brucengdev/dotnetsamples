using Microsoft.Extensions.Options;

namespace UseCustomScopes;

public class SettingsScope: IDisposable
{
    private readonly SettingsOptions _backupSettings;
    private readonly SettingsOptions _defaultSettings;
    public SettingsScope(IServiceProvider serviceProvider, Action<SettingsOptions> configure)
    {
        _defaultSettings = serviceProvider.GetRequiredService<IOptions<SettingsOptions>>().Value;
        _backupSettings = _defaultSettings.Clone();
        
        //change the default settings using passed in action
        configure.Invoke(_defaultSettings);
    }

    public void Dispose()
    {
        //restore the settings back
        _defaultSettings.SettingA = _backupSettings.SettingA;
        _defaultSettings.SettingB = _backupSettings.SettingB;
        _defaultSettings.SettingC = _backupSettings.SettingC;
    }
}