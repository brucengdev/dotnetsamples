public static class ProgramExtensions
{
    public static void Greet(this Program prog)
    {
        Console.WriteLine("Hello");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        (new Program()).Greet();
    }
}