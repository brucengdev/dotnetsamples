using Microsoft.EntityFrameworkCore;

namespace Issues_DbContextIssue;

public interface ITransientService
{
    Task DoStuff();
}

public class TransientService: ITransientService
{
    private readonly IServiceProvider _serviceProvider;
    public TransientService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task DoStuff()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<EFTestDbContext>();
            var students = await dbContext.Students.ToListAsync();
            Console.WriteLine("Number of students: {0}", students.Count);
        }
    }
}