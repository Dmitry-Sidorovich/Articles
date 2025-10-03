﻿using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.DataAccess.Repositories;

public class Repository<TEntity, TContext> :IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    protected TContext DbContext;
    protected DbSet<TEntity> DbSet;

    public Repository(TContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }
    
    public IQueryable<TEntity> GetAll()
    {
       return DbSet;
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }
}