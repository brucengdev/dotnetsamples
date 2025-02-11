namespace TestSubject;

public interface IGreeter
{
    
}
public class Greeter: IGreeter
{
    public string Greet()
    {
        return "Hello";
    }
}

public static class IGreeterExtensions
{
    public static string GreetVN(this IGreeter greeter)
    {
        return "Xin chào!";
    }
}