using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.DataAccess.Repositories;

public interface IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    IQueryable<TEntity> GetAll();
    
    Task AddAsync(TEntity entity);
    
    Task DeleteAsync(Guid id);
    
    Task UpdateAsync(TEntity entity);
    
    Task<TEntity> GetByIdAsync(Guid id);
}