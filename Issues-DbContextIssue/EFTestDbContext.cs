
using Issues_DbContextIssue;
using Microsoft.EntityFrameworkCore;

public class EFTestDbContext: DbContext {
    public DbSet<Student>? Students { get; set; }

    public EFTestDbContext(DbContextOptions<EFTestDbContext> options):base(options) {
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
    }
}