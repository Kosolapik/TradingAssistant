using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlDbContext>
{
    public PostgreSqlDbContext CreateDbContext(string[] args)
    {
        // DbContext сам знает, как настроиться
        var optionsBuilder = new DbContextOptionsBuilder<PostgreSqlDbContext>();
        return new PostgreSqlDbContext(optionsBuilder.Options);
    }
}