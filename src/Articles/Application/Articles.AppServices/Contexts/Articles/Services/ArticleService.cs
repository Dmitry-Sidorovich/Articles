using Articles.AppServices.Contexts.Articles.Builder;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.Contracts.Articles;

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

    public Task<ArticleDto> CreateAsync(CreateArticleDto article)
    {
        return _articleRepository.CreateAsync(article);
    }

    public Task<ArticleDto?> UpdateAsync(Guid id, UpdateArticleDto article)
    {
        return _articleRepository.UpdateAsync(id, article);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return _articleRepository.DeleteAsync(id);
    }
}