using Articles.AppServices.Contexts.Articles.Services;
using Articles.Contracts.Articles;
using Articles.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Hosts.Api.Controllers;

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

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ArticleDto>> CreateArticle(CreateArticleDto article)
    {
        var articleDto = await _articleService.CreateAsync(article);
        if (articleDto == null)
        {
            return BadRequest();
        }
        
        
        return CreatedAtAction(nameof(GetArticleById), new { id = articleDto.Id }, articleDto);
    }
    
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArticleDto>> UpdateArticle([FromRoute] Guid id, [FromBody] UpdateArticleDto article)
    {
        var articleDto = await _articleService.UpdateAsync(id, article);
        
        return articleDto is null ? NotFound() : Ok(articleDto);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArticle(Guid id)
    {
        var articleDto = await _articleService.DeleteAsync(id);
        return articleDto ? NoContent() : NotFound();
    }
}