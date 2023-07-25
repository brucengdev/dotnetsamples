using CustomConfigurationProviderExample.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomConfigurationProviderExample.EFConfigurationProvider;

public class EFConfigurationContext: DbContext
{
    public EFConfigurationContext(DbContextOptions<EFConfigurationContext> options) : base(options)
    {
        
    }

    public DbSet<EFConfigurationValue> Values => Set<EFConfigurationValue>();
}