using Final.DTOs.Request;
using Final.DTOs.Response;
using Final.Entities;

namespace Final.Services.Interfaces;

public interface IUserService
{
    List<UserDto> GetAll();
    UserDto? GetById(int id);
    bool EmailIsTaken(string email);
    bool EmailIsTakenByOtherUser(string email, int userId);
    UserDto Create(CreateUserDto dto);
    User? Authenticate(string email, string password);
    UserDto? Update(int id, UpdateUserDto dto);
    bool Delete(int id);
}