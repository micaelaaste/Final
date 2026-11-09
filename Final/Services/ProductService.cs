using Final.DTOs.Request;
using Final.DTOs.Response;
using Final.Entities;
using Final.Repositories.Interfaces;
using Final.Services.Interfaces;

namespace Final.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryService _categoryService;

    public ProductService(IProductRepository productRepository, ICategoryService categoryService)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
    }

    public ProductDto? GetById(int id)
    {
        Product? product = _productRepository.GetById(id);

        if (product == null)
        {
            return null;
        }

        return ToDto(product);
    }

    public List<ProductDto> GetByUserId(int userId)
    {
        return _productRepository.GetByUserId(userId)
            .Select(p => ToDto(p))
            .ToList();
    }

    public List<ProductDto> GetByCategoryId(int categoryId)
    {
        return _productRepository.GetByCategoryId(categoryId)
            .Select(p => ToDto(p))
            .ToList();
    }

    public List<ProductDto> GetFavourites(int userId)
    {
        return _productRepository.GetFavouritesByUserId(userId)
            .Select(p => ToDto(p))
            .ToList();
    }

    public List<ProductDto> GetDiscounted(int userId)
    {
        return _productRepository.GetDiscountedByUserId(userId)
            .Select(p => ToDto(p))
            .ToList();
    }
    public List<MenuCategoryDto> GetMenu(int userId)
    {
        List<CategoryDto> categories = _categoryService.GetByUserId(userId);
        List<ProductDto> products = GetByUserId(userId);

        return categories.Select(c => new MenuCategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Products = products.Where(p => p.CategoryId == c.Id).ToList()
        }).ToList();
    }
    public bool BelongsToUser(int productId, int userId)
    {
        Product? product = _productRepository.GetById(productId);
        return product != null && product.Category.UserId == userId;
    }
    public bool CategoryBelongsToUser(int categoryId, int userId)
    {
        return _categoryService.BelongsToUser(categoryId, userId);
    }
    public ProductDto Create(CreateProductDto dto)
    {
        Product product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            IsFavourite = dto.IsFavourite,
            Discount = 0,          
            HasHappyHour = false,  
            CategoryId = dto.CategoryId
        };

        _productRepository.Add(product);

        return GetById(product.Id)!;
    }

    public ProductDto Update(int id, UpdateProductDto dto)
    {
        Product product = _productRepository.GetById(id)!;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.IsFavourite = dto.IsFavourite;
        product.CategoryId = dto.CategoryId; 

        _productRepository.Update(product);

        return GetById(id)!;
    }

    public void Delete(int id)
    {
        Product product = _productRepository.GetById(id)!;
        _productRepository.Delete(product);
    }

    public ProductDto UpdateDiscount(int id, UpdateDiscountDto dto)
    {
        Product product = _productRepository.GetById(id)!;

        product.Discount = dto.Discount;
        _productRepository.Update(product);

        return ToDto(product);
    }

    public ProductDto UpdateHappyHour(int id, UpdateHappyHourDto dto)
    {
        Product product = _productRepository.GetById(id)!;

        product.HasHappyHour = dto.HasHappyHour; 
        _productRepository.Update(product);

        return ToDto(product);
    }
    public List<ProductDto> IncreasePrices(int userId, IncreasePricesDto dto)
    {
        List<Product> products = _productRepository.GetByUserId(userId);

        foreach (Product product in products)
        {
            product.Price = product.Price + product.Price * dto.Percentage / 100;
        }

        _productRepository.UpdateAll(products); // un solo SaveChanges para todos

        return products.Select(p => ToDto(p)).ToList();
    }

    private ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Discount = product.Discount,
            FinalPrice = product.Price - product.Price * product.Discount / 100,
            IsFavourite = product.IsFavourite,
            HasHappyHour = product.HasHappyHour,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name
        };
    }
}