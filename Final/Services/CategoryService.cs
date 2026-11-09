using Final.DTOs.Request;
using Final.DTOs.Response;
using Final.Entities;
using Final.Repositories.Interfaces;
using Final.Services.Interfaces;

namespace Final.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public List<CategoryDto> GetByUserId(int userId)
    {
        return _categoryRepository.GetByUserId(userId)
            .Select(c => ToDto(c))
            .ToList();
    }

    public CategoryDto? GetById(int id)
    {
        Category? category = _categoryRepository.GetById(id);

        if (category == null)
        {
            return null;
        }

        return ToDto(category);
    }
    public bool BelongsToUser(int categoryId, int userId)
    {
        Category? category = _categoryRepository.GetById(categoryId);
        return category != null && category.UserId == userId;
    }

    public CategoryDto Create(int userId, CreateCategoryDto dto)
    {
        Category category = new Category
        {
            Name = dto.Name,
            UserId = userId
        };

        _categoryRepository.Add(category);

        return ToDto(category);
    }

    public CategoryDto Update(int id, UpdateCategoryDto dto)
    {
        Category category = _categoryRepository.GetById(id)!;

        category.Name = dto.Name;
        _categoryRepository.Update(category);

        return ToDto(category);
    }

    public void Delete(int id)
    {
        Category category = _categoryRepository.GetById(id)!;
        _categoryRepository.Delete(category); 
    }

    private CategoryDto ToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}