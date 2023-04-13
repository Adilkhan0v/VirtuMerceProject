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

    public AuthService(IUserProvider userProvider, IOptions<SecretOptions> secretOptions)
    {
        _userProvider = userProvider;
        _secretOptions = secretOptions.Value;
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
}