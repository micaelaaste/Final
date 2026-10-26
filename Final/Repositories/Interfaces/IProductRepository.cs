using Final.Entities;

namespace Final.Repositories.Interfaces;

public interface IProductRepository
{
    Product? GetById(int id);
    List<Product> GetByUserId(int userId);
    List<Product> GetByCategoryId(int categoryId);
    List<Product> GetFavouritesByUserId(int userId);
    List<Product> GetDiscountedByUserId(int userId);
    void Add(Product product);
    void Update(Product product);
    void UpdateAll(List<Product> products);
    void Delete(Product product);
}
