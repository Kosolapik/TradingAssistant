using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TradingAssistant.Infrastructure.DataBase.MySQL;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Получаем строку подключения из appsettings.json
        string connectionString = Config.ConnectionStringMySQL();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}