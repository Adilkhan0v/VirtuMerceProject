using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public interface IProductService
{
    Task<ProductDto> GetProductById(Guid id, Guid userId);
    Task DeleteProduct(Guid id, Guid userId);
    Task<Guid> CreateProduct(ProductDto productDto);
    Task UpdateProduct(ProductDto productDto);
    Task<List<ProductDto>> GetAll();
}