using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradingAssistant.Core.Interfaces.Repositories.Exchanges.Abstractions;
using TradingAssistant.Infrastructure.DataBase.PostgreSQL;
using TradingAssistant.Infrastructure.DataBase.PostgreSQL.Repositories.Exchanges.Abstractions;
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

        // Регистрируем UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Регистрация Crypto клиента
        services.AddSingleton<ICryptoClient, CryptoClient>();

        // Регистрация репозиториев (если есть)

        return services;
    }
}