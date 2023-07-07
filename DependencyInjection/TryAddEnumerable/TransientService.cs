namespace TryAddEnumerable
{
    public interface ITransientService {
        string GetMessage();
    }

    public class TransientService : ITransientService
    {
        private int _randomNumber = (new Random()).Next(0, 100);
        public string GetMessage()
        {
            return $"From transient service, random number if {_randomNumber}";
        }
    }

    public class TransientService2 : ITransientService
    {
        private int _randomNumber = (new Random()).Next(0, 100);
        public string GetMessage()
        {
            return $"From TransientService2, random number if {_randomNumber}";
        }
    }

    public class TransientService3 : ITransientService
    {
        private int _randomNumber = (new Random()).Next(0, 100);
        public string GetMessage()
        {
            return $"From TransientService3, random number if {_randomNumber}";
        }
    }
}