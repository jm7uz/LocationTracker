using LocationTracker.Domain.Commons;
using System.Security.AccessControl;

namespace LocationTracker.Data.IRepositories;

public interface IRepository<TEntity, TKey> where TEntity : Auditable<TKey>
{
    Task<bool> DeleteAsync(TKey id);
    IQueryable<TEntity> SelectAll();
    Task<TEntity> SelectByIdAsync(TKey id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task SaveAsync();
}
