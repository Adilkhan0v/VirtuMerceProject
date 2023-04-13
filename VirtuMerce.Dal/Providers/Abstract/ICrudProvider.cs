using VirtuMerce.Dal.Entities.Abstract;

namespace VirtuMerce.Dal.Providers.Abstract;

public interface ICrudProvider<TEntity>where TEntity : BaseEntity
{
    Task Create(TEntity temp);
    Task<TEntity> GetById(Guid id);
    Task<List<TEntity>> GetAll();
    Task Update(TEntity entity);
    Task Delete(Guid id);
    Task CreateMany(List<TEntity> temp);
}