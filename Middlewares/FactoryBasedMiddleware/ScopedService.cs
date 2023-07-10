namespace FactoryBasedMiddleware;

public interface IScopedService
{
    string GetMessage();
}

public class ScopedService: IScopedService
{
    public String GetMessage() => "This is from a scoped service";
}