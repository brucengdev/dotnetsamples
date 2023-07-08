using Microsoft.EntityFrameworkCore;

namespace Issues_DbContextIssue;

public interface IScopedService
{
    Task DoStuff();
}

public class ScopedService: IScopedService
{
    private readonly EFTestDbContext _dbContext;
    public ScopedService(EFTestDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task DoStuff()
    {
        var students = await _dbContext.Students.ToListAsync();
        Console.WriteLine("[Scoped service] Number of students: {0}", students.Count);
    }
}