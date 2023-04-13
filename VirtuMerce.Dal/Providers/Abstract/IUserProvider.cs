using VirtuMerce.Dal.Entities;

namespace VirtuMerce.Dal.Providers.Abstract;

public interface IUserProvider : ICrudProvider<UserEntity>
{
    Task<UserEntity?>GetByLogin(string login);
}


