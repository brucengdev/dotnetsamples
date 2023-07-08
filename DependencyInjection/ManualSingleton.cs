namespace DependencyInjection
{
    public class ManualSingleton: IDisposable
    {
        public string GetMessage()
        {
            return "This class is not created by the service container but created by developer instead";
        }

        public void Dispose()
        {
            //disposed will not be called automatically by the DI container, developers need to do this
            //themselves
        } 
    }
}
