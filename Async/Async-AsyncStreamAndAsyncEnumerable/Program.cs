
static async IAsyncEnumerable<string> GetWords()
{
    string data = @"This is the first line
                  This is the second line failed
                    This is the last line";
    using var reader = new StringReader(data);
    var line = await reader.ReadLineAsync();
    while (line != null)
    {
        var words = line.Split(' ');
        foreach (var word in words)
        {
            if (word != "")
            {
                yield return word;
            }
            if(word == "failed") {
                yield break;
            }
        }
        line = await reader.ReadLineAsync();
    }
}

await foreach (var word in GetWords())
{
    Console.WriteLine("Word: {0}", word);
}