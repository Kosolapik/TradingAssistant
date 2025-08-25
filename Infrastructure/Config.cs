using Microsoft.Extensions.Configuration;

namespace TradingAssistant.Infrastructure;

internal static class Config
{
    private static readonly Lazy<IConfiguration> _config = new(LoadConfiguration);

    private static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public static string ConnectionStringPostgreSQL()
    {
        var connectionString = _config.Value["DatabaseConnections:PostgreSQL"];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Database connection string is not configured");

        return connectionString;
    }
}

