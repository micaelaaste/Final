using Final.DTOs.Request;
using Final.DTOs.Response;
using Final.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        CategoryDto? category = _categoryService.GetById(id);

        if (category == null)
        {
            return NotFound("La categoría no existe.");
        }

        return Ok(category);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(CreateCategoryDto dto)
    {
        CategoryDto created = _categoryService.Create(GetUserId(), dto);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, UpdateCategoryDto dto)
    {
        if (_categoryService.GetById(id) == null)
        {
            return NotFound("La categoría no existe.");
        }

        if (!_categoryService.BelongsToUser(id, GetUserId()))
        {
            return Forbid();
        }

        return Ok(_categoryService.Update(id, dto));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        if (_categoryService.GetById(id) == null)
        {
            return NotFound("La categoría no existe.");
        }

        if (!_categoryService.BelongsToUser(id, GetUserId()))
        {
            return Forbid();
        }

        _categoryService.Delete(id);

        return NoContent();
    }

    private int GetUserId()
    {
        return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}