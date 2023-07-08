using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRelatedServices(this IServiceCollection services)
        {
            //multiple instances of a service type can be registered
            //the instances can be resolved with IEnumerable<TService>
            //they appear in the order they are registered
            services.AddSingleton<ISingletonDep, SingletonDep>();
            services.AddSingleton<ISingletonDep, SingletonDep2>();
            //the following will only register if no service was registered
            //in this case it does not do anything
            services.TryAddSingleton<ISingletonDep, SingletonDep2>();

            services.AddScoped<IScopedDep, ScopedDep>();
            services.AddTransient<ITransientDep, TransientDep>();
            services.AddSingleton(new ManualSingleton());//manual singleton needs to be disposed
            //of manually by developer, since it was not created by the service container
            services.AddScoped<ServiceScopeFactoryExample>();
            return services;
        }
    }
}
