using Articles.AppServices.Contexts.Articles.Repository;
using Articles.Contracts.Articles;

namespace Articles.AppServices.Contexts.Articles.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    
    public Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> CreateAsync(CreateArticleDto article)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> UpdateAsync(Guid id, UpdateArticleDto article)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}