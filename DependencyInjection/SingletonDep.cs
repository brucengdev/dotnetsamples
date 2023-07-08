namespace DependencyInjection
{
    public interface ISingletonDep
    {
        string GetMessage();
    }
    public class SingletonDep: ISingletonDep
    {
        private int _randomNumber = (new Random()).Next(0, 100);
        public string GetMessage()
        {
            return $"This is from SingletonDep, value is {_randomNumber}";
        }
    }

    public class SingletonDep2 : ISingletonDep
    {
        private int _randomNumber = (new Random()).Next(0, 100);
        public string GetMessage()
        {
            return $"This is from SingletonDep2, value is {_randomNumber}";
        }
    }
}
