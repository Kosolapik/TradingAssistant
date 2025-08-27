using Microsoft.EntityFrameworkCore.Storage;
using TradingAssistant.Core.Exceptions;
using TradingAssistant.Core.Interfaces.Repositories.Exchanges.Abstractions;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Repositories.Exchanges.Abstractions;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgreSqlDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(PostgreSqlDbContext context)
    {
        _context = context;
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T);

        if (!_repositories.TryGetValue(type, out var repository))
        {
            repository = new Repository<T>(_context);
            _repositories.Add(type, repository);
        }

        return (IRepository<T>)repository;
    }

    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        if (_currentTransaction != null)
            throw new InvalidOperationException("Транзакция уже начата");

        _currentTransaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        // Неявная транзакция (если нет активной явной)
        return await _context.SaveChangesAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct = default)
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("Нет активной транзакции");

        try
        {
            await _context.SaveChangesAsync(ct);
            await _currentTransaction.CommitAsync(ct);
        }
        catch
        {
            await RollbackAsync(ct);
            throw;
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task RollbackAsync(CancellationToken ct = default)
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("No active transaction");

        await _currentTransaction.RollbackAsync(ct);
        await _currentTransaction.DisposeAsync();
        _currentTransaction = null;
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _context.Dispose();
    }
}
