namespace FactoryBasedMiddleware;

public static class ServiceCollectionExtensions
{
    public static IApplicationBuilder UseConventionalMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ConventionalMiddleware>();
        return builder;
    }

    public static IApplicationBuilder UseFactoryActivatedMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<FactoryActivatedMiddleware>();
        return builder;
    }
}