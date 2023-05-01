using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public class ProductService : IProductService
{
    private readonly IProductProvider _productProvider;
    private readonly IUserProvider _userProvider;

    public ProductService(IProductProvider productProvider, IUserProvider userProvider)
    {
        _productProvider = productProvider;
        _userProvider = userProvider;
    }
    
    public async Task<ProductDto> GetProductById(Guid id, Guid userId)
    {
        
        var product = await _productProvider.GetById(id);

        if (product.User.Id != userId)
        {
            throw new ArgumentException("Log in first!");
        }

        return new ProductDto(product.Id, product.Title, product.Details, product.Price, product.User.Id);
    }

    public async Task DeleteProduct(Guid id, Guid userId)
    {
        var productEntity = await _productProvider.GetById(id);
        if (productEntity.User.Id != userId)
        {
            throw new ArgumentException("Log in first!");
        }
        
        await _productProvider.Delete(id);
    }

    public async Task<Guid> CreateProduct(ProductDto productDto)
    { 
        var user = await _userProvider.GetById(productDto.UserId);
        
       await _productProvider.Create(new ProductEntity
        {
            Title = productDto.Title,
            Details = productDto.Details,
            Price = productDto.Price,
            User = user
        });
       return productDto.Id;
    }

    public async Task UpdateProduct(ProductDto productDto)
    {
        var user = await _userProvider.GetById(productDto.UserId);

        if (productDto.UserId != user.Id)
        {
            throw new ArgumentException("User not found");
        }
        
        var product = await _productProvider.GetById(productDto.Id);
        product.Details = productDto.Details;
        product.Title = productDto.Title;
        product.Price = productDto.Price;
        await _productProvider.Update(product);
    }

    public async Task<List<ProductDto>> GetAll()
    {
        var fromDb = await _productProvider.GetAll();
        return fromDb.Select(x => new ProductDto(x.Id, x.Title, x.Details, x.Price, x.User.Id)).ToList();
    }
}