namespace FactoryBasedMiddleware;

public class CustomMiddlewareFactory : IMiddlewareFactory
{
    public IMiddleware? Create(Type middlewareType)
    {
        Console.WriteLine("Custom middleware factory is invoked");
        throw new NotImplementedException();
    }

    public void Release(IMiddleware middleware)
    {
        throw new NotImplementedException();
    }
}