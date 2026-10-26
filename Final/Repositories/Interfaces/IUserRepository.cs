using Final.Entities;

namespace Final.Repositories.Interfaces;

public interface IUserRepository
{
    List<User> GetAll();
    User? GetById(int id);
    User? GetByEmail(string email);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
}
