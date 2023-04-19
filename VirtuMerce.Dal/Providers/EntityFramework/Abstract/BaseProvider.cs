using Microsoft.EntityFrameworkCore;
using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Entities.Abstract;
using VirtuMerce.Dal.Providers.Abstract;

namespace VirtuMerce.Dal.Providers.EntityFramework.Abstract;

public abstract class BaseProvider<TEntity> : ICrudProvider<TEntity> where TEntity:BaseEntity
{
    protected readonly ApplicationContext _applicationContext;
    private readonly DbSet<TEntity> _dbSet;

    protected BaseProvider(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _dbSet = _applicationContext.Set<TEntity>();
    }

    public async Task Create(TEntity temp)
    {
        _dbSet.Add(temp);
       await _applicationContext.SaveChangesAsync();
    }

    public Task<TEntity> GetById(Guid id)
    {
        return _dbSet.FirstAsync(x=>x.Id==id);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Update(TEntity entity)
    {
        _applicationContext.Entry(entity).State = EntityState.Modified;
        await _applicationContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var element =await _dbSet.FindAsync(id);
        _dbSet.Remove(element);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task CreateMany(List<TEntity> temp)
    {
        await _dbSet.AddRangeAsync(temp);
        await _applicationContext.SaveChangesAsync();
    }
}