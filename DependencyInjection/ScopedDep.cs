namespace DependencyInjection
{
    public interface IScopedDep
    {
        string GetMessage();
    }
    public class ScopedDep: IScopedDep
    {
        private int randomNumber = (new Random()).Next(0, 10000);
        public string GetMessage()
        {
            return $"From scoped service, message is: {randomNumber}";
        }
    }
}
