using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public interface IAuthService
{
    public Task<string> Signup(UserSignupDto userSignupDto);

}