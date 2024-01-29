public class MyClass
{
    [field: ThreadStatic] public static string Message1 { get; set; }
    
    public static string Message2 { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        var rand = new Random();

        var task = () =>
        {
            MyClass.Message2 = $"Thread{rand.Next(1000)}";
            Console.WriteLine("Thread {0}: Message2 = {1}", Thread.CurrentThread.ManagedThreadId, MyClass.Message2);
        };
        Parallel.Invoke(task, task, task, task, task, task, task, task);

        Console.WriteLine("=====================");
        Console.WriteLine("Notice static prop with ThreadStatic will not have duplicates, each thread has its own values");
        
        task = () =>
        {
            MyClass.Message1 = $"Thread{rand.Next(1000)}";
            Console.WriteLine("Thread {0}: Message1 = {1}", Thread.CurrentThread.ManagedThreadId, MyClass.Message1);
        };
        Parallel.Invoke(task, task, task, task, task, task, task, task);
    }
}