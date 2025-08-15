using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradingAssistant.Infrastructure.DataBase.MySQL;
using TradingAssistant.Infrastructure.Exchanges.Crypto;

namespace TradingAssistant.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services
    ){
        // Получаем строку подключения из appsettings.json
        string connectionString = Config.ConnectionStringMySQL();

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
        );

        // Регистрация Crypto клиента
        services.AddSingleton<ICryptoClient, CryptoClient>();

        // Регистрация репозиториев (если есть)

        return services;
    }
}