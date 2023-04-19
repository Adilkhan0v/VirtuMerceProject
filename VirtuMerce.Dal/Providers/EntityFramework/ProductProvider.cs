using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VirtuMerce.Dal.Providers.EntityFramework.Abstract;

namespace VirtuMerce.Dal.Providers.EntityFramework;

public class ProductProvider : BaseProvider<ProductEntity>, IProductProvider
{
    private readonly ApplicationContext _applicationContext;
    
    public ProductProvider(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }
}