using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TradingAssistant.Infrastructure;

namespace TradingAssistant.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services
    )
    {
        services.AddInfrastructure();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
