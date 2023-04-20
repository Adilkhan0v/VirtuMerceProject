using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VirtuMerce.Dal.Providers.EntityFramework.Abstract;

namespace VirtuMerce.Dal.Providers.EntityFramework;

public class ProductProvider : BaseProvider<ProductEntity>, IProductProvider
{
    public ProductProvider(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
}