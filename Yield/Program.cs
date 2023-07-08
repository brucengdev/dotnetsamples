//yield is specific to IEnumerable<T>
IEnumerable<int> GetFibonacciNumbers(int n)
{
    yield return 1;
    yield return 1;
    int last = 1, secondLast = 1;
    for (int i = 2; i <= n; i++)
    {
        var number = last + secondLast;
        yield return number;
        last = secondLast;
        secondLast = number;
    }
}

foreach (var number in GetFibonacciNumbers(10))
{
    Console.Write("{0} ", number);
}