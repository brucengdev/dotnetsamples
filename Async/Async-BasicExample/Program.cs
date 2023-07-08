/***
 * Basically
 * Async programming is a way to avoid wasting the main thread being while waiting for external resources
 * like I/O, network.
 * The UI will hang while the thread waits for results from external resources.
 * By doing something else when the operation is waiting to complete, you utilize the thread.
 * It's basically similar to promises in Javascript.
 * Instead of blocking the main thread by waiting for the external resource to return. When a method await 
 */

using System.Diagnostics;

async Task CookEggAsync() {
    Console.WriteLine("Heating fan and cooking egg");
    await Task.Delay(3000);
}

async Task BoilWaterAsync() {
    Console.WriteLine("Boiling water");
    await Task.Delay(4000);
}
void MakeCoffee() {
    Console.WriteLine("Making coffee");
    Thread.Sleep(1000);//this is synchronous and CPU bound task
}

async Task ToastBreadsAsync() {
    Console.WriteLine("Start toasting breads");
    await Task.Delay(3000);
}

async Task ToastBreadsWithExceptionAsync() {
    Console.WriteLine("Start toasting breads");
    await Task.Delay(3000);
    Console.WriteLine("Toaster catches fire, breads are ruined");
    throw new InvalidOperationException("Toaster is on fire");
    //exceptions will be caught by the caller when it awaits the returned task
}

void PrepareSandwich() {
    Console.WriteLine("Start preparing sandwich");
    Thread.Sleep(1000);
}

async Task SyncBreakfast() {
    var watch = new Stopwatch();
    Console.WriteLine("Making a synchronous breakfast");
    watch.Start();
    await BoilWaterAsync();
    Console.WriteLine("Water is boiled");
    MakeCoffee();
    Console.WriteLine("Coffee is ready");
    await CookEggAsync();
    Console.WriteLine("Eggs are ready");
    await ToastBreadsAsync();
    Console.WriteLine("Breads are toasted");
    PrepareSandwich();
    Console.WriteLine("Sandwich is ready");
    watch.Stop();
    Console.WriteLine("Finished, took {0} seconds", Math.Floor(watch.Elapsed.TotalSeconds));
}

async Task AsyncBreakfastWithWhenAll() {
    //making a breakfast asynchronously will be faster
    //because you will do other work while waiting for water to boil, for example.
    //- Waiting for current task to be done => blocking the thread.
    //- Do something else while a task is going on => non-blocking.
    var watch = new Stopwatch();
    Console.WriteLine("Making an asynchronous breakfast, wait for all async tasks");
    watch.Start();
    var boilWaterTask = BoilWaterAsync();
    var cookEggTask = CookEggAsync();
    var toastBreadTask = ToastBreadsAsync();
    await Task.WhenAll(boilWaterTask, cookEggTask, toastBreadTask);
    Console.WriteLine("Water is boiled, eggs are ready and breads are toasted");
    MakeCoffee();
    Console.WriteLine("Coffee is ready");
    PrepareSandwich();
    Console.WriteLine("Sandwich is ready");
    watch.Stop();
    Console.WriteLine("Finished, took {0} seconds", Math.Floor(watch.Elapsed.TotalSeconds));
}

async Task AsyncBreakfastWithWhenAny() {
    var watch = new Stopwatch();
    Console.WriteLine("Making an asynchronous breakfast, wait for any task");
    watch.Start();
    var boilWaterTask = BoilWaterAsync();
    var cookEggTask = CookEggAsync();
    var toastBreadTask = ToastBreadsAsync();
    var tasks = new List<Task>{ boilWaterTask, cookEggTask, toastBreadTask };
    while(tasks.Count > 0) {
        var finishedTask = await Task.WhenAny(tasks);
        if(finishedTask == boilWaterTask) {
            Console.WriteLine("Water is boiled");
            MakeCoffee();
            Console.WriteLine("Coffee is ready");
        } else if(finishedTask == cookEggTask) {
            Console.WriteLine("Egg is ready");
            PrepareSandwich();
            Console.WriteLine("Sandwich is ready");
        } else if(finishedTask == toastBreadTask) {
            Console.WriteLine("Breads are toasted");
        }
        await finishedTask;
        //await the task again, to get its result when you needed
        //it's not necessary if you don't need its result.
        tasks.Remove(finishedTask);//wait for the next task that will finish earliest
    }
    watch.Stop();
    Console.WriteLine("Finished, took {0} seconds", Math.Floor(watch.Elapsed.TotalSeconds));
}

void AddButter() {
    Console.WriteLine("Butter added");
}

void AddJams() {
    Console.WriteLine("Jams added");
}

//an operation that contains both async ops and sync ops is an async operation
async Task MakeToastWithButterAndJam() {
    await ToastBreadsAsync();
    Console.WriteLine("Breads are toasted");
    AddButter();
    AddJams();
}

await SyncBreakfast();
Console.WriteLine();
await AsyncBreakfastWithWhenAll();
Console.WriteLine();
await AsyncBreakfastWithWhenAny();

Console.WriteLine();
Console.WriteLine("Demonstrate composition of tasks");
await MakeToastWithButterAndJam();

Console.WriteLine();
Console.WriteLine("Demonstrate exception handling in async programming");
var taskWithException = ToastBreadsWithExceptionAsync();
Console.WriteLine("The caller has not received the exception, yet");
try {
    await taskWithException;
}catch(Exception ex) {
    Console.WriteLine("Exception caught: " + ex.Message);
    Console.WriteLine("The held exception: " + taskWithException.Exception.Message);
}