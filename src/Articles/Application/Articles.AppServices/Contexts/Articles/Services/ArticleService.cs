using Articles.AppServices.Contexts.Articles.Builder;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.Contracts.Articles;
using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticlePredicateBuilder _predicateBuilder;

    /// <summary>
    /// ,
    /// </summary>
    /// <param name="articleRepository"></param>
    public ArticleService(IArticleRepository articleRepository, IArticlePredicateBuilder predicateBuilder)
    {
        _articleRepository = articleRepository;
        _predicateBuilder = predicateBuilder;
    }
    
    public Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter)
    {
        var query = _predicateBuilder.WithUsers().OrderByTitle().Build(); // пример применения строителя. 
        return _articleRepository.GetByFilterAsync(filter);
    }

    public Task<ArticleDto?> GetByIdAsync(Guid id)
    {
        return _articleRepository.GetByIdAsync(id);
    }

    public Task<Guid> CreateAsync(CreateArticleDto article)
    {
        var entity = new Article
        {
            CreatedAt = article.CreatedAt,
            Description = article.Description,
            Title = article.Title,
            User = new User
            {
                CreatedAt = article.CreatedAt,
                Name = article.UserName,
            }
        };
        return _articleRepository.AddAsync(entity);
    }

    public Task<ArticleDto?> UpdateAsync(Guid id, UpdateArticleDto article)
    {
        return _articleRepository.UpdateAsync(id, article);
    }

    public Task DeleteAsync(Guid id)
    {
        return _articleRepository.DeleteAsync(id);
    }
}