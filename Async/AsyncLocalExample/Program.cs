/*
 * AsyncLocal store values for an async flow.
 * You can have multiple async methods accessing the same instance of AsyncLocal, but each method will get
 * a different value
 */

public class AsyncLocalExample
{
    private static async Task DoAsyncStuff(int taskId, AsyncLocal<int> asyncLocal)
    {
        Console.WriteLine("Task {0}: initializing, setting value", taskId);
        var rand = new Random();
        asyncLocal.Value = rand.Next(10);
        Console.WriteLine("Task {0}: value is set: {1}", taskId, asyncLocal.Value);
        await Task.Delay(2000);
        Console.WriteLine("Task {0}: finishing task..., value is {1}", taskId, asyncLocal.Value);
    }

    private static async Task StartTasks()
    {
        AsyncLocal<int> asyncLocal = new();
        var t1 = DoAsyncStuff(1, asyncLocal);
        var t2 = DoAsyncStuff(2, asyncLocal);
        var t3 = DoAsyncStuff(3, asyncLocal);
        await Task.WhenAll(t1, t2, t3);
    }
    
    public static void Main(string[] args)
    {
        StartTasks().Wait();
    }
}