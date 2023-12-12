public class TypeA
{
    private string Name { get; set; }
}

public class TypeB: TypeA
{
    public string Name { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        var x = new TypeB();
        var theType = x.GetType();
        var theProp = theType.GetProperty("Name");
    }
}