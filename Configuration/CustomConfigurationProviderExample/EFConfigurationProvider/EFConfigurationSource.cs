using Microsoft.EntityFrameworkCore;

namespace CustomConfigurationProviderExample.EFConfigurationProvider;

//basically the IConfigurationSource is a IConfigurationProvider builder
public class EFConfigurationSource: IConfigurationSource
{
    private readonly Action<DbContextOptionsBuilder> _optionsAction;

    public EFConfigurationSource(Action<DbContextOptionsBuilder> optionsAction) => _optionsAction = optionsAction;
    public IConfigurationProvider Build(IConfigurationBuilder builder)
        => new EFConfigurationProvider(_optionsAction);
}