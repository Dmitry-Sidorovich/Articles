using Articles.Contracts.Articles;
using Articles.AppServices.Specification;
using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Repository;

public interface IArticleRepository
{
    Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter);
    Task<IReadOnlyCollection<ArticleDto>> FindAsync(Specification<ArticleDto> predicate);
    Task<ArticleDto?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(Article article);
    Task<ArticleDto?> UpdateAsync(Guid id, UpdateArticleDto article);
    Task DeleteAsync(Guid id);
}