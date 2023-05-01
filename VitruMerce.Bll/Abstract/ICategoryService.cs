using VirtuMerce.Dal.Entities;
using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public interface ICategoryService
{
    Task<CategoryDto> GetCategoryById(Guid id, Guid userId);
    Task DeleteCategory(Guid id, Guid userId);
    Task<Guid> CreateCategory(CategoryDto categoryDto);
    Task UpdateCategory(CategoryDto categoryDto);
    Task<List<CategoryDto>> GetAll();
}