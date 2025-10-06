using System.Collections.Concurrent;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Contracts.Base;
using Articles.Domain.Entities;
using Articles.Infrastructure.DataAccess.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Articles.Infrastructure.DataAccess.Contexts.Articles.Repositories;

/// <inheritdoc />
public class ArticleRepository(ILogger<ArticleRepository> logger, IRepository<Article, ApplicationDbContext> repository, IMapper mapper) : IArticleRepository
{
    private readonly ConcurrentDictionary<Guid, ArticleDto> _articles = new();
    
    public async Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter)
    {
        var articles = repository.GetAll();

        if (!string.IsNullOrWhiteSpace(filter.Title))
        {
            articles = articles.Where(a => a.Title.Contains(filter.Title));
        }

        if (!string.IsNullOrWhiteSpace(filter.UserName))
        {
            articles = articles.Where(a => a.User.Name.Contains(filter.UserName));
        }

        return await articles
            .ProjectTo<ArticleDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<PaginationCollection<ArticleDto>> FindAsync(Specification<Article> predicate,
        int page,
        int take)
    {
        var query = repository.GetAll().Where(predicate);
        
        var total = await query.CountAsync();

        var result = await query
            .OrderBy(a => a.Id)
            .Skip(take * (page - 1))
            .Take(take)
            .ProjectTo<ArticleDto>(mapper.ConfigurationProvider)
            .ToArrayAsync();

        return new PaginationCollection<ArticleDto>
        {
            Items = result.AsReadOnly(),
            Total = total,
            AvailablePages = (int)double.Round((total / (double)take), MidpointRounding.ToPositiveInfinity) - page,
        };

        /*return await repository
            .GetAll()
            .Where(predicate)
            .OrderBy(a => a.Id)
            .Skip(page)
            .Take(take)
            .ProjectTo<ArticleDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);*/
    }

    /// <inheritdoc />
    public Task<ArticleDto> GetByIdAsync(Guid id)
    {
        return repository.GetAll().Where(s => s.Id == id)
            .Include(s => s.User)
            .ProjectTo<ArticleDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
            // .Select(s => new ArticleDto
            // {
            //     Id = s.Id,
            //     Title = s.Title,
            //     CreatedAt = s.CreatedAt,
            //     Description = s.Description,
            //     UserName = s.User.Name
            // }).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<Guid> AddAsync(Article article)
    {
        await repository.AddAsync(article);
        return article.Id;
    }

    /// <inheritdoc />
    public async Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto request)
    {
        var article = await repository.GetByIdAsync(id);

        if (article is null)
        {
            return new ArticleDto();
        }

        var updatedArticle = mapper.Map(request, article);

        await repository.UpdateAsync(updatedArticle);
        return await GetByIdAsync(id) 
               ?? throw new ApplicationException($"Something went wrong. Updated article not found. Id: {id}");
    }
    
    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}