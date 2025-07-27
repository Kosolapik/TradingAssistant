using Microsoft.Extensions.DependencyInjection;
using TradingAssistant.Infrastructure.Exchanges.Crypto;
using TradingAssistant.Application.CQRS.Exchange.Queries.GetActiveSymbols;
using TradingAssistant.Infrastructure;

namespace TradingAssistant.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services
    )
    {
        services.AddInfrastructure();

        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(Query).Assembly));

        return services;
    }
}
