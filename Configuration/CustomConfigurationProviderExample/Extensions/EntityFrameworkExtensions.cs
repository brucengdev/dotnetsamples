using CustomConfigurationProviderExample.EFConfigurationProvider;
using Microsoft.EntityFrameworkCore;

namespace CustomConfigurationProviderExample.Extensions;

public static class EntityFrameworkExtensions
{
    public static IConfigurationBuilder AddEFConfiguration(
        this IConfigurationBuilder builder,
        Action<DbContextOptionsBuilder> optionsAction)
    {
        return builder.Add(new EFConfigurationSource(optionsAction));
    }
}