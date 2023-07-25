using Microsoft.EntityFrameworkCore;

namespace CustomConfigurationProviderExample.EFConfigurationProvider;

public class EFConfigurationProvider: ConfigurationProvider
{
    private Action<DbContextOptionsBuilder> OptionsAction { get; }
    public EFConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
    {
        OptionsAction = optionsAction;
    }

    public override void Load()
    {
        
    }
}