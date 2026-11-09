using Final.DTOs.Request;
using Final.DTOs.Response;

namespace Final.Services.Interfaces;

public interface ICategoryService
{
    List<CategoryDto> GetByUserId(int userId);
    CategoryDto? GetById(int id);
    bool BelongsToUser(int categoryId, int userId);
    CategoryDto Create(int userId, CreateCategoryDto dto);
    CategoryDto Update(int id, UpdateCategoryDto dto);
    void Delete(int id);
}