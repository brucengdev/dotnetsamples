
//ThreadLocal stores value per each thread in the same variable
var ThreadName = new ThreadLocal<string>(() =>
{
    return $"Thread {Thread.CurrentThread.ManagedThreadId}";
});

var action = () =>
{
    //in this case, we can just use Thread.CurrentThread.Name right here
    //so not quite sure what are some applications of ThreadLocal yet
    Console.WriteLine("Thread {0}", ThreadName.Value);
};

Parallel.Invoke(action, action, action, action, action, action, action);

ThreadName.Dispose();
