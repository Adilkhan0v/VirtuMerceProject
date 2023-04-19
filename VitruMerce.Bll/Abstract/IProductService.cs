using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public interface IProductService
{
    Task<ProductDto> GetProductById(Guid id);
    Task DeleteProduct(Guid id);
    Task CreateProduct(ProductDto productDto);
    Task UpdateProduct(ProductDto productDto);
    Task<List<ProductDto>> GetAll();
}