using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Interfaces.Repositories.Exchanges.Abstractions;
using TradingAssistant.Infrastructure.DataBase.PostgreSQL;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly PostgreSqlDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(PostgreSqlDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync([ id! ], ct);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(ct);
    }

    public virtual IQueryable<T> AsQueryable() => _dbSet.AsQueryable();

    public virtual void Add(T entity) => _dbSet.Add(entity);
    public virtual void Update(T entity) => _dbSet.Update(entity);
    public virtual void Remove(T entity) => _dbSet.Remove(entity);

    public virtual async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}