namespace DependencyInjection
{
    public interface ITransientDep
    {
        string GetMessage();
    }
    public class TransientDep: ITransientDep
    {
        private int _randomNumber = (new Random()).Next(0, 1000);
        public string GetMessage()
        {
            return $@"From transient dependency, number is {_randomNumber}";
        }
    }
}
