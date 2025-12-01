using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KeplerDemo.Infrastructure;

public class BaseUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    protected readonly TContext _context;

    public BaseUnitOfWork(TContext context)
    {
        _context = context;
    }

    public virtual async Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update<TEntity>(TEntity entity) where TEntity : class
    {
        _context.Set<TEntity>().Update(entity);
    }

    public virtual void Remove<TEntity>(TEntity entity) where TEntity : class
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public virtual async Task<TEntity?> FindByIdAsync<TEntity>(object id, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await _context.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken);
    }

    public virtual IQueryable<TEntity> GetAsQueryable<TEntity>() where TEntity : class
    {
        return _context.Set<TEntity>().AsQueryable();
    }

    public virtual async Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return predicate == null
            ? await _context.Set<TEntity>().ToListAsync(cancellationToken)
            : await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual int Commit()
    {
        return _context.SaveChanges();
    }

    public virtual async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}