namespace HostAndStartup
{
    public class Dependency : IDependency
    {
        public string GetValue()
        {
            return "This is a dependency";
        }
    }
}
