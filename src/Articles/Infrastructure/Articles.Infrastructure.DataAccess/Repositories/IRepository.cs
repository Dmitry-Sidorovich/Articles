using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.DataAccess.Repositories;

/// <summary>
/// Базовый репозиторий.
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности.</typeparam>
/// <typeparam name="TContext">Тип <see cref="DbContext">контекста данных</see>.</typeparam>
public interface IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// Получить последовательность элементов.
    /// </summary>
    /// <returns>Объект постройки запросов к последовательности элементов.</returns>
    IQueryable<TEntity> GetAll();
    
    /// <summary>
    /// Добавить сущность.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    Task AddAsync(TEntity entity);
    
    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    Task DeleteAsync(Guid id);
    
    /// <summary>
    /// Обновить сущность.
    /// </summary>
    /// <param name="entity"></param>
    Task UpdateAsync(TEntity entity);
    
    /// <summary>
    /// Получить сущность.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Доменная модель.</returns>
    Task<TEntity?> GetByIdAsync(Guid id);
}