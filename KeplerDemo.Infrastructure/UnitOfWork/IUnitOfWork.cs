using System.Data;
using System.Linq.Expressions;

namespace KeplerDemo.Infrastructure;

public interface IUnitOfWork
{
    Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
    Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;
    void Update<TEntity>(TEntity entity) where TEntity : class;
    void Remove<TEntity>(TEntity entity) where TEntity : class;
    Task<TEntity?> FindByIdAsync<TEntity>(object id, CancellationToken cancellationToken = default) where TEntity : class;
    IQueryable<TEntity> GetAsQueryable<TEntity>() where TEntity : class;
    Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) where TEntity : class;
    int Commit();
    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    //IDbConnection GetDbConnection();
}
