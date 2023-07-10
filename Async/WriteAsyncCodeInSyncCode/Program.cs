namespace WriteAsyncCodeInSyncCode;

public class Program {

    private static string GetContent() {
        using(var client = new HttpClient()) {
            var response = client.GetAsync("https://vnexpress.net").GetAwaiter().GetResult();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return content;
        }
    }

    public static void Main(string[] args) {
        Console.WriteLine("Getting content from the web...");
        var content = GetContent();
        Console.WriteLine("Content: {0}", content.Substring(0, 1000));
    }
}