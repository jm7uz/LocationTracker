using LocationTracker.Data.DbContexts;
using LocationTracker.Data.IRepositories;
using LocationTracker.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace LocationTracker.Data.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Auditable<TKey>
{
    protected readonly LocationTrackerDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(LocationTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await _dbSet.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(TKey id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
        _dbSet.Remove(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public IQueryable<TEntity> SelectAll()
        => _dbSet;

    public async Task<TEntity> SelectByIdAsync(TKey id)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
