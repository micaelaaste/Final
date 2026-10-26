using Final.Data;
using Final.Entities;
using Final.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly FinalContext _context;

    public ProductRepository(FinalContext context)
    {
        _context = context;
    }

    // Include trae también la categoría, para poder mostrar CategoryName en el ProductDto
    public Product? GetById(int id)
    {
        return _context.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.Id == id);
    }

    // El producto llega al restaurante a través de su categoría
    public List<Product> GetByUserId(int userId)
    {
        return _context.Products
            .Include(p => p.Category)
            .Where(p => p.Category.UserId == userId)
            .ToList();
    }

    public List<Product> GetByCategoryId(int categoryId)
    {
        return _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .ToList();
    }

    public List<Product> GetFavouritesByUserId(int userId)
    {
        return _context.Products
            .Include(p => p.Category)
            .Where(p => p.Category.UserId == userId && p.IsFavourite)
            .ToList();
    }

    // "Con descuento o happy hour": cualquiera de las dos condiciones
    public List<Product> GetDiscountedByUserId(int userId)
    {
        return _context.Products
            .Include(p => p.Category)
            .Where(p => p.Category.UserId == userId && (p.Discount > 0 || p.HasHappyHour))
            .ToList();
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.SaveChanges();
    }

    // Para el aumento de precios: el service modifica todos y se guardan juntos con un solo SaveChanges
    public void UpdateAll(List<Product> products)
    {
        _context.SaveChanges();
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}
