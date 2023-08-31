//declare the delegate type here
//the delegate matches any method that returns an int and takes in two int parameters
delegate int NumericFunction(int x, int y);

delegate void CallBack(string result);

internal class DelegateClassExample {

    public static int Multiply(int x, int y)
    {
        return x * y;
    }

    public int Addition(int x, int y)
    {
        return x + y;
    }
    
    public void FetchHttpContent(string url, CallBack callBack)
    {
        Console.WriteLine("Fetching content from {0}", url);
        var message = "";
        using (var client = new HttpClient())
        {
            var result = client.GetAsync(url).GetAwaiter().GetResult();
            message = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
        //notify caller using the callback
        callBack(message);
    }

    public void Execute()
    {
        //assign delegate variable to a static method
        NumericFunction op = Multiply;
        Console.WriteLine("Executing a static method: {0} x {1} = {2}", 12, 13, op(12, 13));

        //assign delegate variable to an instance method
        op = Addition;
        Console.WriteLine("Executing an instance method: {0} + {1} = {2}", 5,6, op(5,6));
        
        //assign delegate variable to a delegate using lambda expression
        op = (x, y) => x / y;
        Console.WriteLine("Executing a lambda expression: {0} / {1} = {2}", 12, 4, op(12, 4));
        
        //pattern of using callbacks, the delegate is created using a lambda expression
        FetchHttpContent("https://vnexpress.net", (message) =>
        {
            Console.WriteLine("Received message");
            Console.WriteLine("{0}...", message.Substring(0, 200));
        });
        
        //Example of a single delegate invoking multiple methods, this is called multicasting
        Console.WriteLine();
        Console.WriteLine("Example with multicasting");
        CallBack firstCallBack = (result) => Console.WriteLine("This is first callback, result is {0}", result);
        CallBack secondCallBack = (result) => Console.WriteLine("This is second callback, result is {0}", result);
        CallBack multicastDelegate = firstCallBack + secondCallBack;
        multicastDelegate("Chicken");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        (new DelegateClassExample()).Execute();
    }
}