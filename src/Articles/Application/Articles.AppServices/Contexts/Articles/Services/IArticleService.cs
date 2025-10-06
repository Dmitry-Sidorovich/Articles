﻿using Articles.Contracts.Articles;
using Articles.Contracts.Base;

namespace Articles.AppServices.Contexts.Articles.Services;

/// <summary>
/// Сервис для работы со статьями.
/// </summary>
public interface IArticleService
{
    /// <summary>
    /// Получить статьи по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <returns>Коллекция моделей статей.</returns>
    Task<PaginationCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter);
    
    /// <summary>
    /// Получить модель статьи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Модель статьи.</returns>
    Task<ArticleDto?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Создаёт статью по модели.
    /// </summary>
    /// <param name="article">Модель статьи.</param>
    /// <returns>Идентификатор созданной статьи.</returns>
    Task<Guid> CreateAsync(CreateArticleDto article);
    
    /// <summary>
    /// Обновляет статью по модели.
    /// </summary>
    /// <param name="id">Идентификатор существующей статьи.</param>
    /// <param name="request">Модель обновления.</param>
    /// <returns>Модель обновлённой статьи.</returns>
    Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto request);
    
    /// <summary>
    /// Удаляет статью.
    /// </summary>
    /// <param name="id">Идентификатор статьи.</param>
    Task DeleteAsync(Guid id);
}