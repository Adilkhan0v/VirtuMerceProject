using Microsoft.EntityFrameworkCore;
using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Entities.Abstract;
using VirtuMerce.Dal.Providers.Abstract;
using VirtuMerce.Dal.Providers.EntityFramework.Abstract;

namespace VirtuMerce.Dal.Providers.EntityFramework;

public class UserProvider : BaseProvider<UserEntity>, IUserProvider
{
    private readonly ApplicationContext _applicationContext;

    public UserProvider(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<UserEntity?> GetByLogin(string login)
    {
        return await _applicationContext.Users.FirstOrDefaultAsync(x => x.Login == login);   
    }
    
}