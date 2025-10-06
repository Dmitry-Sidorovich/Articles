using Articles.AppServices.Contexts.Articles.Builder;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Contexts.Articles.Specification;
using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Contracts.Base;
using Articles.Domain.Entities;
using AutoMapper;

namespace Articles.AppServices.Contexts.Articles.Services;

/// <inheritdoc />
public class ArticleService(
    IArticleRepository articleRepository,
    IArticlePredicateBuilder predicateBuilder,
    IMapper mapper)
    : IArticleService
{
    /// <inheritdoc />
    public Task<PaginationCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter)
    {
        //return articleRepository.GetByFilterAsync(filter);
        Specification<Article> specification = Specification<Article>.True;

        if (filter.Title is not null)
        {
            specification = specification.And(new ArticleTitleSpecification(filter.Title));
        }

        if (filter.UserName is not null)
        {
            specification = specification.And(new ArticleUserNameSpecification(filter.UserName));
        }
        
        return articleRepository.FindAsync(specification, filter.Page, filter.Take);
    }
    
    /// <inheritdoc />
    public Task<ArticleDto?> GetByIdAsync(Guid id)
    {
        return articleRepository.GetByIdAsync(id);
    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(CreateArticleDto article)
    {
        var entity = mapper.Map<CreateArticleDto, Article>(article);
        return articleRepository.AddAsync(entity);
    }

    /// <inheritdoc />
    public Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto request)
    {
        return articleRepository.UpdateAsync(id, request);
    }

    /// <inheritdoc />
    public Task<ArticleDto?> UpdateAsync(Guid id, UpdateArticleDto article)
    {
        return articleRepository.UpdateAsync(id, article);
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id)
    {
        return articleRepository.DeleteAsync(id);
    }
}