using VirtuMerce.Dal.Entities;
using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public interface ICategoryService
{
    Task<CategoryDto> GetCategoryById(Guid id);
    Task DeleteCategory(Guid id);
    Task CreateCategory(CategoryDto categoryDto);
    Task UpdateCategory(CategoryDto categoryDto);
    Task<List<CategoryDto>> GetAll();
}