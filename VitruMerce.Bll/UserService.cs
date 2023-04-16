using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public class UserService : IUserService
{
    private readonly IUserProvider _userProvider;

    public UserService(IUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _userProvider.GetById(id);

        return new UserDto(user.Id, user.Login, user.Username);
    }

    public async Task<UserDto> GetUserByLogin(string login)
    {
        var user = await _userProvider.GetByLogin(login);
        
        return new UserDto(user.Id, user.Login, user.Username);
    }
}