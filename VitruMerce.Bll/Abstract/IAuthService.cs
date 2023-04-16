using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public interface IAuthService
{
    public Task<string> Signup(UserSignupDto userSignupDto);
    public Task<string> Signin(UserSigninDto userSigninDto);
    
    /// <summary>
    /// Gets User by headers from Request
    /// Usage in controllers: 
    /// GetUserByHeaders(Request.Headers[HeaderNames.Authorization].ToArray())
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<UserDto> GetUserByHeaders(string[] headers);
}