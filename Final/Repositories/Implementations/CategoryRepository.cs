using Final.Data;
using Final.Entities;
using Final.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly FinalContext _context;

    public CategoryRepository(FinalContext context)
    {
        _context = context;
    }

    public List<Category> GetByUserId(int userId)
    {
        return _context.Categories
            .Where(c => c.UserId == userId)
            .ToList();
    }

    public List<Category> GetByUserIdWithProducts(int userId)
    {
        return _context.Categories
            .Include(c => c.Products)   // Include pide tambien que se traiga la categoría. Si no está, vuelve null
            .Where(c => c.UserId == userId)
            .ToList();
    }

    public Category? GetById(int id)
    {
        return _context.Categories.FirstOrDefault(c => c.Id == id);
    }

    public void Add(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void Update(Category category)
    {
        _context.SaveChanges();
    }

    // Por el borrado en cascada se borran también sus productos
    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }
}