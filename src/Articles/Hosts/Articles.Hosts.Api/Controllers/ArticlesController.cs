﻿using Articles.AppServices.Contexts.Articles.Services;
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
    
    [HttpGet("by-filter")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ArticleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetArticlesByFilter([FromQuery] ArticleFilterDto filter)
    {
        //throw new ValidationException("Invalid filter provided"); - для проверки middleware
        var articles = await _articleService.GetByFilterAsync(filter);
        if (articles.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(articles);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetArticleById(Guid id)
    {
        var article = await _articleService.GetByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        
        return Ok(article);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IReadOnlyCollection<ArticleDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateArticle(CreateArticleDto article)
    {
        var articleDto = await _articleService.CreateAsync(article);
        if (articleDto == null)
        {
            return BadRequest();
        }
        
        
        return CreatedAtAction(nameof(GetArticleById), new { id = articleDto.Id }, articleDto);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ArticleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateArticle(Guid id, UpdateArticleDto article)
    {
        var articleDto = await _articleService.UpdateAsync(id, article);
        if (articleDto == null)
        {
            return BadRequest();
        }
        
        
        return Ok(articleDto);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ArticleDto>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteArticle(Guid id)
    {
        var articleDto = await _articleService.DeleteAsync(id);
        if (articleDto == false) // ???
        {
            return BadRequest();
        }
        
        
        return NoContent();
    }
}