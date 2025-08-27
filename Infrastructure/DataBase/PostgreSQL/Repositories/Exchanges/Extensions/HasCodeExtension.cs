using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Interfaces.Repositories.Exchanges.MarkersInterfaces;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Repositories.Exchanges.Extensions;

public static class HasCodeExtension
{
    public static async Task<T?> GetByCodeAsync<T>(
        this IHasCodeRepository<T> repository,
        string code,
        CancellationToken cancellationToken = default) where T : class
    {
        return await repository.AsQueryable()
            .FirstOrDefaultAsync(e => EF.Property<string>(e, "Code") == code, cancellationToken);
    }

    public static async Task<bool> CodeExistsAsync<T>(
        this IHasCodeRepository<T> repository,
        string code,
        CancellationToken cancellationToken = default) where T : class
    {
        return await repository.AsQueryable()
            .AnyAsync(e => EF.Property<string>(e, "Code") == code, cancellationToken);
    }

    public static async Task<T> GetOrCreateAsync<T>(
        this IHasCodeRepository<T> repository,
        string code,
        Func<T> createFactory,
        CancellationToken cancellationToken = default) where T : class
    {
        var existingEntity = await repository.GetByCodeAsync(code, cancellationToken);
        return existingEntity ?? createFactory();
    }
}
