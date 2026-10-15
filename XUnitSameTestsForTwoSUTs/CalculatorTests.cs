using Shouldly;

namespace XUnitSameTestsForTwoSUTs;

/*
    An example of using the same test suite to run tests on two different implementations (SUTs)
*/

public interface ICalculator
{
    int Add(int v1, int v2);
    object Multiply(int v1, int v2);
    object Subtract(int v1, int v2);
    object Divide(int v1, int v2);
}

public class Calculator : ICalculator
{
    public int Add(int v1, int v2)
    {
        return v1 + v2;
    }

    public object Divide(int v1, int v2)
    {
        return v1 / v2;
    }

    public object Multiply(int v1, int v2)
    {
        return v1 * v2;
    }

    public object Subtract(int v1, int v2)
    {
        return v1 - v2;
    }
}

public class CalculatorWithLogging : ICalculator
{
    public int Add(int v1, int v2)
    {
        var result = v1 + v2;
        Console.WriteLine("{0} + {1} = {2}", v1, v2, result);
        return result;
    }

    public object Divide(int v1, int v2)
    {
        var result = v1 / v2;
        Console.WriteLine("{0} / {1} = {2}", v1, v2, result);
        return result;
    }

    public object Multiply(int v1, int v2)
    {
        var result = v1 * v2;
        Console.WriteLine("{0} x {1} = {2}", v1, v2, result);
        return result;
    }

    public object Subtract(int v1, int v2)
    {
        var result = v1 - v2;
        Console.WriteLine("{0} - {1} = {2}", v1, v2, result);
        return result;
    }
}

public abstract class BaseTestSuite
{
    protected abstract ICalculator CreateSUT();

    [Fact]
    public void TestAddition()
    {
        var sut = CreateSUT();
        var result = sut.Add(12, 23);

        result.ShouldBe(35);
    }

    [Fact]
    public void TestSubtraction()
    {
        var sut = CreateSUT();
        var result = sut.Subtract(33, 3);

        result.ShouldBe(30);
    }

    [Fact]
    public void TestMultiplication()
    {
        var sut = CreateSUT();
        var result = sut.Multiply(2, 22);

        result.ShouldBe(44);
    }

    [Fact]
    public void TestDivision()
    {
        var sut = CreateSUT();
        var result = sut.Divide(30, 5);

        result.ShouldBe(6);
    }
}


public class CalculatorTests : BaseTestSuite
{
    protected override ICalculator CreateSUT()
    {
        return new Calculator();
    }
}

public class CalculatorWithLoggingTests : BaseTestSuite
{
    protected override ICalculator CreateSUT()
    {
        return new CalculatorWithLogging();
    }
}
