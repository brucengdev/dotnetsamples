namespace Middleware_Dependencies
{
    public interface IDependency
    {
        string Message { get; }
    }
    public interface ISingletonDependency: IDependency
    {
    }

    public interface ITransientDependency: IDependency
    {

    }

    public interface IScopedDependency: IDependency
    {

    }


    public class BaseDependency
    {
        public Guid Id { get; set; }
        public BaseDependency()
        {
            Id = Guid.NewGuid();
        }
    }

    public class SingletonDependency : BaseDependency, ISingletonDependency
    {
        public string Message {
            get
            {
                return "SingletonDependency, ID = " + Id.ToString();
            }
        }
    }


    public class TransientDependency: BaseDependency, ITransientDependency {
        public string Message
        {
            get
            {
                return "TransientDependency, ID = " + Id.ToString();
            }
        }
    }
    public class ScopedDependency: BaseDependency, IScopedDependency {
        public string Message
        {
            get
            {
                return "ScopedDependency, ID = " + Id.ToString();
            }
        }
    }
    
}
