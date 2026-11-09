using Final.DTOs.Request;
using Final.DTOs.Response;
using Final.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        ProductDto? product = _productService.GetById(id);

        if (product == null)
        {
            return NotFound("El producto no existe.");
        }

        return Ok(product);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(CreateProductDto dto)
    {
        if (!_productService.CategoryBelongsToUser(dto.CategoryId, GetUserId()))
        {
            return BadRequest("La categoría no existe o no pertenece a tu restaurante.");
        }

        ProductDto created = _productService.Create(dto);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        int userId = GetUserId();

        if (_productService.GetById(id) == null)
        {
            return NotFound("El producto no existe.");
        }

        if (!_productService.BelongsToUser(id, userId))
        {
            return Forbid();
        }

        if (!_productService.CategoryBelongsToUser(dto.CategoryId, userId))
        {
            return BadRequest("La categoría no existe o no pertenece a tu restaurante.");
        }

        return Ok(_productService.Update(id, dto));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        if (_productService.GetById(id) == null)
        {
            return NotFound("El producto no existe.");
        }

        if (!_productService.BelongsToUser(id, GetUserId()))
        {
            return Forbid();
        }

        _productService.Delete(id);

        return NoContent();
    }

    [HttpPut("{id}/discount")]
    [Authorize]
    public IActionResult UpdateDiscount(int id, UpdateDiscountDto dto)
    {
        if (_productService.GetById(id) == null)
        {
            return NotFound("El producto no existe.");
        }

        if (!_productService.BelongsToUser(id, GetUserId()))
        {
            return Forbid();
        }

        return Ok(_productService.UpdateDiscount(id, dto));
    }

    [HttpPut("{id}/happy-hour")]
    [Authorize]
    public IActionResult UpdateHappyHour(int id, UpdateHappyHourDto dto)
    {
        if (_productService.GetById(id) == null)
        {
            return NotFound("El producto no existe.");
        }

        if (!_productService.BelongsToUser(id, GetUserId()))
        {
            return Forbid();
        }

        return Ok(_productService.UpdateHappyHour(id, dto));
    }

    private int GetUserId()
    {
        return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}