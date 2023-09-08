public class Student: IEquatable<Student>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public bool Equals(Student? other)
    {
        if (other == null)
        {
            return false;
        }
        return GetHashCode() == other.GetHashCode();
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Name);
        hashCode.Add(Age);
        return hashCode.ToHashCode();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var students = new List<Student>
        {
            new() { Name = "Tom", Age = 12 },
            new() { Name = "Tom", Age = 12 },
            new() { Name = "Jane", Age = 12 },
            new() { Name = "Jane", Age = 14 }
        };

        foreach (var student in students)
        {
            Console.WriteLine("{0} - {1}: {2}", student.Name, student.Age, student.GetHashCode());
        }

        Console.WriteLine();
        students = students.Distinct().ToList();

        foreach (var student in students)
        {
            Console.WriteLine("{0} - {1}", student.Name, student.Age);
        }
    }
}