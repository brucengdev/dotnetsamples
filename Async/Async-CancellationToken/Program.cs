var tokenSource = new CancellationTokenSource();
var token = tokenSource.Token;

async Task GenerateFibonacci(int n, CancellationToken token) {
    var lastValue = 0L;
    var value = 1L;
    for(int i = 0; i < n; i++) {
        if(token.IsCancellationRequested) {
            Console.WriteLine("Operation cancelled");
            break;
        }
        var backup = value;
        value = value + lastValue;
        lastValue = backup;
        Console.WriteLine("{0} ", value);
        await Task.Delay(100);
    }
}

var generateTask = GenerateFibonacci(200, token);

async Task cancelTask(CancellationTokenSource tokenSource) {
    do {
        Console.WriteLine("Press ENTER to cancel the operation...");
    }while(Console.ReadKey().Key != ConsoleKey.Enter);
    Console.WriteLine("Cancelling operation after 2 more seconds");
    tokenSource.CancelAfter(2000);
    //or cancel right away with tokenSource.Cancel()
}

await Task.WhenAll(new List<Task>{generateTask, cancelTask(tokenSource) });