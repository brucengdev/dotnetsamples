public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }

    public bool IsEqual(Student other)
    {
        //the private method can be invoked from the same class even on a different instance
        return Hash() == other.Hash();
    }
    
    private int Hash()
    {
        var hashCode = new HashCode();
        hashCode.Add(Name);
        hashCode.Add(Age);
        return hashCode.ToHashCode();
    }
}

public class Program
{
    public static void Main()
    {
        
    }
}