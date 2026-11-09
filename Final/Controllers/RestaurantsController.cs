using Final.DTOs.Request;
using Final.DTOs.Response;
using Final.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public RestaurantsController(IUserService userService, ICategoryService categoryService, IProductService productService)
    {
        _userService = userService;
        _categoryService = categoryService;
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        RestaurantDto? restaurant = _userService.GetById(id);

        if (restaurant == null)
        {
            return NotFound("El restaurante no existe.");
        }

        return Ok(restaurant);
    }

    [HttpPost]
    public IActionResult Create(CreateUserDto dto)
    {
        if (_userService.EmailIsTaken(dto.Email))
        {
            return Conflict("Ya existe un restaurante registrado con ese email.");
        }

        UserDto created = _userService.Create(dto);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, UpdateUserDto dto)
    {
        if (id != GetUserId())
        {
            return Forbid();
        }

        if (_userService.EmailIsTakenByOtherUser(dto.Email, id))
        {
            return Conflict("Ese email ya está en uso por otro restaurante.");
        }

        UserDto? updated = _userService.Update(id, dto);

        if (updated == null)
        {
            return NotFound("El restaurante no existe.");
        }

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        if (id != GetUserId())
        {
            return Forbid();
        }

        if (!_userService.Delete(id))
        {
            return NotFound("El restaurante no existe.");
        }

        return NoContent();
    }

    [HttpGet("{id}/menu")]
    public IActionResult GetMenu(int id)
    {
        if (_userService.GetById(id) == null)
        {
            return NotFound("El restaurante no existe.");
        }

        return Ok(_productService.GetMenu(id));
    }

    [HttpGet("{id}/categories")]
    public IActionResult GetCategories(int id)
    {
        if (_userService.GetById(id) == null)
        {
            return NotFound("El restaurante no existe.");
        }

        return Ok(_categoryService.GetByUserId(id));
    }

    [HttpGet("{id}/products")]
    public IActionResult GetProducts(int id, int? categoryId)
    {
        if (_userService.GetById(id) == null)
        {
            return NotFound("El restaurante no existe.");
        }

        if (categoryId == null)
        {
            return Ok(_productService.GetByUserId(id));
        }

        if (!_categoryService.BelongsToUser(categoryId.Value, id))
        {
            return NotFound("La categoría no existe en este restaurante.");
        }

        return Ok(_productService.GetByCategoryId(categoryId.Value));
    }

    [HttpGet("{id}/products/favourites")]
    public IActionResult GetFavourites(int id)
    {
        if (_userService.GetById(id) == null)
        {
            return NotFound("El restaurante no existe.");
        }

        return Ok(_productService.GetFavourites(id));
    }

    [HttpGet("{id}/products/discounted")]
    public IActionResult GetDiscounted(int id)
    {
        if (_userService.GetById(id) == null)
        {
            return NotFound("El restaurante no existe.");
        }

        return Ok(_productService.GetDiscounted(id));
    }

    [HttpPut("{id}/prices")]
    [Authorize]
    public IActionResult IncreasePrices(int id, IncreasePricesDto dto)
    {
        if (id != GetUserId())
        {
            return Forbid();
        }

        return Ok(_productService.IncreasePrices(id, dto));
    }

    private int GetUserId()
    {
        return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}