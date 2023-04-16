using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public interface IUserService
{
    Task<UserDto> GetUserById(Guid id);
    Task<UserDto> GetUserByLogin(string login);
}