using Final.Entities;

namespace Final.Repositories.Interfaces;

public interface ICategoryRepository
{
    List<Category> GetByUserId(int userId);
    List<Category> GetByUserIdWithProducts(int userId);
    Category? GetById(int id);
    void Add(Category category);
    void Update(Category category);
    void Delete(Category category);
}
