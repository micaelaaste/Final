using Final.DTOs.Request;
using Final.DTOs.Response;
using Final.Entities;
using Final.Repositories.Interfaces;
using Final.Services.Interfaces;

namespace Final.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<UserDto> GetAll()
    {
        return _userRepository.GetAll()
            .Select(u => ToDto(u)) 
            .ToList();
    }

    public UserDto? GetById(int id)
    {
        User? user = _userRepository.GetById(id);

        if (user == null)
        {
            return null;
        }

        return ToDto(user);         // PREGUNTAR A NICO SI SE PUEDE USAR ASI O SI HACEMOS EL MAPEO DENTRO DE CADA METODO recibe una entidad y devuelve un dto de salida con lo mismo pero que puede ver el cliente 
    }
    public bool EmailIsTaken(string email)
    {
        return _userRepository.GetByEmail(email) != null;
    }

    public bool EmailIsTakenByOtherUser(string email, int userId)
    {
        User? existing = _userRepository.GetByEmail(email);
        return existing != null && existing.Id != userId;
    }

    public UserDto Create(CreateUserDto dto)
    {
        User user = new User
        {
            Name = dto.Name,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), 
            RestaurantName = dto.RestaurantName,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber
        };

        _userRepository.Add(user); // después del SaveChanges, user.Id ya tiene valor

        return ToDto(user);
    }

    public User? Authenticate(string email, string password)
    {
        User? user = _userRepository.GetByEmail(email);

        if (user == null)
        {
            return null;
        }

        if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return user;
        }

        return null;
    }

    public UserDto? Update(int id, UpdateUserDto dto)
    {
        User? user = _userRepository.GetById(id);

        if (user == null)
        {
            return null;
        }

        user.Name = dto.Name;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        user.RestaurantName = dto.RestaurantName;
        user.Address = dto.Address;
        user.PhoneNumber = dto.PhoneNumber;

        _userRepository.Update(user);

        return ToDto(user);
    }

    public bool Delete(int id)
    {
        User? user = _userRepository.GetById(id);

        if (user == null)
        {
            return false;
        }

        _userRepository.Delete(user); 
        return true;
    }

    private UserDto ToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            RestaurantName = user.RestaurantName,
            Address = user.Address,
            PhoneNumber = user.PhoneNumber
        };
    }
}