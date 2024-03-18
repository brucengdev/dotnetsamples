public class Program
{
    private static async Task<IEnumerable<TResult>> ExecuteInBatches<T, TResult>(
        int batchSize,
        IEnumerable<T> input,
        Func<IEnumerable<T>, Task<IEnumerable<TResult>>> doStuff)
    {
        List<TResult> results = new();
        var batches = input.Chunk(batchSize);
        foreach (var batch in batches)
        {
            results.AddRange(await doStuff(batch));
        }

        return results;
    }
    
    public static async Task Main(string[] args)
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        var results = await ExecuteInBatches(2, numbers, async itemsInBatches =>
        {
            return itemsInBatches.Select(num => num * 2);
        });
        Console.WriteLine(string.Join(",", results));
    }
}