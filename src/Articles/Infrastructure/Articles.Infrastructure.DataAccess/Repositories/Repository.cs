using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.DataAccess.Repositories;

/// <inheritdoc />
public class Repository<TEntity, TContext> :IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    protected TContext DbContext;
    protected DbSet<TEntity> DbSet;

    /// <summary>
    /// Инициализирует экземпляр <see cref="Repository{TEntity, TContext}"/>.
    /// </summary>
    public Repository(TContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }
    
    /// <inheritdoc />
    public IQueryable<TEntity> GetAll()
    {
       return DbSet;
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }
}