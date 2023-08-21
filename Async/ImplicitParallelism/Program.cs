// Refer to https://blog.stephencleary.com/2017/03/aspnetcore-synchronization-context.html

var messages = new List<string>();

async Task AddMessage(string message)
{
    Console.WriteLine("Adding message {0}", message);
    await Task.Delay(3000);
    //this part can run in a different threads
    messages.Add(message);
}

// The continuation may run in a different thread in ASP.NET Core
Console.WriteLine("Checking for current thread before and after async task");
Console.WriteLine("Thread: {0}", Environment.CurrentManagedThreadId);
await AddMessage("test");
Console.WriteLine("Thread: {0}", Environment.CurrentManagedThreadId);

//
messages = new List<string>();
//because some continuations run in parallel, there maybe issue when multiple
//threads try to modify messages
//though it is very rare
var tasks = new List<Task>
{
    AddMessage("A"),
    AddMessage("B"),
    AddMessage("C")
};
await Task.WhenAll(tasks);
Console.WriteLine("Results");
foreach (var message in messages)
{
    Console.WriteLine("Message: {0}", message);
}