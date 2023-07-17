namespace UseCustomScopes;

public class SettingsOptions
{
    public int SettingA { get; set; }
    public int SettingB { get; set; }
    public string SettingC { get; set; }

    public SettingsOptions Clone() => new()
    {
        SettingA = this.SettingA,
        SettingB = this.SettingB,
        SettingC = this.SettingC
    };
}