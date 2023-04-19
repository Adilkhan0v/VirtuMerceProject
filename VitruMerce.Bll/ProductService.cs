using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public class ProductService : IProductService
{
    private readonly IProductProvider _productProvider;

    public ProductService(IProductProvider productProvider)
    {
        _productProvider = productProvider;
    }
    
    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await _productProvider.GetById(id);

        return new ProductDto(product.Id, product.Title, product.Details, product.Price);
    }

    public async Task DeleteProduct(Guid id)
    {
       await _productProvider.Delete(id);
    }

    public async Task CreateProduct(ProductDto productDto)
    {
       await _productProvider.Create(new ProductEntity
        {
            Title = productDto.Title,
            Details = productDto.Details,
            Price = productDto.Price
        });
    }

    public async Task UpdateProduct(ProductDto productDto)
    {
        var product = await _productProvider.GetById(productDto.Id);
        product.Details = productDto.Details;
        product.Title = productDto.Title;
        product.Price = productDto.Price;
        await _productProvider.Update(product);
    }

    public async Task<List<ProductDto>> GetAll()
    {
        var fromDb = await _productProvider.GetAll();
        return fromDb.Select(x => new ProductDto(x.Id, x.Title, x.Details, x.Price)).ToList();
    }
}