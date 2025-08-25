using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradingAssistant.Infrastructure.DataBase.PostgreSQL;
using TradingAssistant.Infrastructure.Exchanges.Crypto;

namespace TradingAssistant.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services
    ){
        // PostgreSQL
        // Простая регистрация DbContext
        // Конфигурация происходит в самом DbContext
        services.AddDbContext<PostgreSqlDbContext>();

        // Регистрация Crypto клиента
        services.AddSingleton<ICryptoClient, CryptoClient>();

        // Регистрация репозиториев (если есть)

        return services;
    }
}