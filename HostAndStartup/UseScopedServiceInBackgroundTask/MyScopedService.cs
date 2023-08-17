namespace UseScopedServiceInBackgroundTask;

public class MyScopedService: IMyScopedService
{
    public string GetMessage()
    {
        return "Chicken curry";
    }
}