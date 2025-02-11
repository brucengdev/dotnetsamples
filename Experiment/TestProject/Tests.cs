using TestSubject;

namespace TestProject;

public class Tests
{
    [Fact]
    public void Test1()
    {
        var sut = new Greeter();
        
        Assert.Equal("Xin chào!", sut.GreetVN());
    }
}