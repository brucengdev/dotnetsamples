using Microsoft.Extensions.Options;

namespace BindHierarchicalConfigWithOptionsPattern;

public class SingletonService: ISingletonService
{
    private readonly IOptionsMonitor<PositionOptions> _positionOptions;
    public SingletonService(IOptionsMonitor<PositionOptions> positionOptions)
    {
        _positionOptions = positionOptions;
    }

    public string Title
    {
        get => _positionOptions.CurrentValue.Title;
    }

    public string Name
    {
        get => _positionOptions.CurrentValue.Name;
    }
}