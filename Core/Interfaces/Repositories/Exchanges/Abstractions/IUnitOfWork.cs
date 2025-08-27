namespace TradingAssistant.Core.Interfaces.Repositories.Exchanges.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : class;

    Task BeginTransactionAsync(CancellationToken ct = default);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
    Task RollbackAsync(CancellationToken ct = default);
}