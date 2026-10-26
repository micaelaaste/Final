using Final.Data;
using Final.Entities;
using Final.Repositories.Interfaces;

namespace Final.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly FinalContext _context;

    public UserRepository(FinalContext context)
    {
        _context = context;
    }

    public List<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? GetById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    // Igual que en el ejemplo de JWT de la cátedra: se compara sin importar mayúsculas
    public User? GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    // El service ya modificó las propiedades; EF detecta los cambios y hace el UPDATE
    public void Update(User user)
    {
        _context.SaveChanges();
    }

    // Por el borrado en cascada se borran también sus categorías y productos
    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}
