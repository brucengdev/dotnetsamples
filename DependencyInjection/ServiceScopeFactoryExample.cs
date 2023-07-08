namespace DependencyInjection
{
    public class ServiceScopeFactoryExample
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ServiceScopeFactoryExample(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public string GetMessage()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var transientService = scope.ServiceProvider.GetRequiredService<ITransientDep>();
                return $"Message from transient service resolved from scope: {transientService.GetMessage()}";
            }
        }
    }
}
