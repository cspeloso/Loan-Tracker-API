using FinanceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinanceAPI.Data;

public class FinanceDbContextFactory : IDesignTimeDbContextFactory<FinanceDbContext>
{
    public FinanceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FinanceDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=finance;Username=finance;Password=finance"
        );

        return new FinanceDbContext(optionsBuilder.Options);
    }
}