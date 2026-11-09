using Final.DTOs.Request;
using Final.DTOs.Response;

namespace Final.Services.Interfaces;
public interface IProductService
{
    ProductDto? GetById(int id);
    List<ProductDto> GetByUserId(int userId);
    List<ProductDto> GetByCategoryId(int categoryId);
    List<ProductDto> GetFavourites(int userId);
    List<ProductDto> GetDiscounted(int userId);
    List<MenuCategoryDto> GetMenu(int userId);

    bool BelongsToUser(int productId, int userId);
    bool CategoryBelongsToUser(int categoryId, int userId);

    ProductDto Create(CreateProductDto dto);
    ProductDto Update(int id, UpdateProductDto dto);
    void Delete(int id);
    ProductDto UpdateDiscount(int id, UpdateDiscountDto dto);
    ProductDto UpdateHappyHour(int id, UpdateHappyHourDto dto);
    List<ProductDto> IncreasePrices(int userId, IncreasePricesDto dto);
}