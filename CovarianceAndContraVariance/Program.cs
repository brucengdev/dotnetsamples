
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class Doctor: Person { }

delegate Person GetPerson(string name, int age);

public class Program { 

    public static Doctor GetDoctor(object name, int age)
    {
        return new Doctor { Name = "Dr " + name, Age = age };
    }

    public static void Main(string[] args)
    {
        //covariance is when a method with return value of a derived type is assigned to 
        //a delegate with return value of a more general type
        //in this case Doctor GetDoctor is assigned to Person GetPerson

        //contravariance is when a method with parameters of a more general type is assigned
        //to a delegate with parameters of derived types
        //in this case GetDoctor(object, int) is assigned to delegate GetPerson(string, int)
        //This is useful when you want to use a single handler for multiple different events(delegates)
        GetPerson dele = GetDoctor;
        var person = dele("Thomas", 33);
        Console.WriteLine("Name: {0}, Age: {1}", person.Name, person.Age);
    }
}