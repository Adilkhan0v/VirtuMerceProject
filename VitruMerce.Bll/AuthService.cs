using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VirtuMerce.Contracts.Options;
using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public class AuthService : IAuthService
{
    private readonly IUserProvider _userProvider;
    private readonly SecretOptions _secretOptions;
    private readonly IUserService _userService;

    public AuthService(IUserProvider userProvider, IOptions<SecretOptions> secretOptions, IUserService userService)
    {
        _userProvider = userProvider;
        _secretOptions = secretOptions.Value;
        _userService = userService;
    }
    
    public async Task<string> Signup(UserSignupDto userSignupDto)
    {

        if (await _userProvider.GetByLogin(userSignupDto.Login) is not null)
        {
            throw new ArgumentException("User with requested Login is already exist");
        }

        await _userProvider.Create(new UserEntity
        {
            Login = userSignupDto.Login,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userSignupDto.Password),
            Username = userSignupDto.Username
        });


        return GenerateToken(userSignupDto.Login, userSignupDto.Username);
    }

    public async Task<string> Signin(UserSigninDto userSigninDto)
    {
        var user = await _userProvider.GetByLogin(userSigninDto.Login);
        if (user is null)
        {
            throw new ArgumentException("Invalid login or password");
        }

        if (BCrypt.Net.BCrypt.Verify(userSigninDto.Password, user.PasswordHash) == false)
        {
            throw new ArgumentException("Invalid login or password");
        }
        
        return GenerateToken(user.Login,user.Username);
    }


    private string GenerateToken(string login, string username)
    {
        var key = Encoding.ASCII.GetBytes(_secretOptions.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, login)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
    
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    
    /// <inheritdoc /> 
    public async Task<UserDto> GetUserByHeaders(string[] headers)
    {
        var token = headers[0].Replace("Bearer ", "");
        var login = DecryptToken(token).Login;

        return await _userService.GetUserByLogin(login);
    }
    
    
    /// <summary>
    ///    Token decryption
    /// </summary>
    /// <param name="token"></param>
    /// <exception cref="ArgumentException">throws when could not parse claims</exception>
    /// <returns>Owner's data</returns>
    private (string Login, string Username) DecryptToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        if (tokenS?.Claims is List<Claim> claims)
        {
            return new ValueTuple<string, string>(claims[0].Value, claims[1].Value);
        }

        throw new ArgumentException();
    }
}