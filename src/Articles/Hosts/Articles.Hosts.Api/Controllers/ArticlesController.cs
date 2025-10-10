using System.Net;
using Articles.AppServices.Contexts.Articles.Services;
using Articles.Contracts.Articles;
using Articles.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы со статьями.
/// </summary>
/// <param name="articleService"></param>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;
    
    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }
    
    /// <summary>
    /// List of articles by filter (pagination/sorting in query)
    /// </summary>
    /// <param name="filter">filter</param>
    /// <returns>articles dto</returns>
    [HttpGet("by-filter")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ArticleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<ArticleDto>>> GetArticlesByFilter([FromQuery] ArticleFilterDto filter)
    {
        //throw new ValidationException("Invalid filter provided"); - для проверки middleware
        var articles = await _articleService.GetByFilterAsync(filter);
        if (articles.Items.Count == 0)
        {
            return NotFound();
        }
        return Ok(articles);
    }

    /// <summary>
    /// Get article by id
    /// </summary>
    /// <param name="id">article's id</param>
    /// <returns>article dto</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArticleDto>> GetArticleById(Guid id)
    {
        var article = await _articleService.GetByIdAsync(id);
        return article is null ? NotFound() : Ok(article);
    }

    /// <summary>
    /// Создаёт статью по модели.
    /// </summary>
    /// <param name="article">Модель создания статьи.</param>
    /// <returns>Идентификатор созданной статьи.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateArticle(CreateArticleDto article)
    {
        var id = await _articleService.CreateAsync(article);
        return StatusCode(StatusCodes.Status201Created, id);
    }
    
    /// <summary>
    /// Обновляет статью по модели.
    /// </summary>
    /// <param name="id">Идентификатор существующей статьи.</param>
    /// <param name="request">Модель обновления.</param>
    /// <returns>Модель обновлённой статьи.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArticleDto>> UpdateArticle([FromRoute] Guid id, [FromBody] UpdateArticleDto request)
    {
        var articleDto = await _articleService.UpdateAsync(id, request);
        return Ok(articleDto);
    }
    
    /// <summary>
    /// Удаляет статью.
    /// </summary>
    /// <param name="id">Идентификатор статьи.</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArticle(Guid id)
    {
        await _articleService.DeleteAsync(id);
        return NoContent();
    }
}