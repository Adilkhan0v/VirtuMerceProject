using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public class CategoryService : ICategoryService
{
    private readonly ICategoryProvider _categoryProvider;

    public CategoryService(ICategoryProvider categoryProvider)
    {
        _categoryProvider = categoryProvider;
    }
    
    public async Task<CategoryDto> GetCategoryById(Guid id)
    {
        var category = await _categoryProvider.GetById(id);
        
        return new CategoryDto(category.Id, category.Name);
    }

    public async Task DeleteCategory(Guid id)
    {
       await _categoryProvider.Delete(id);
    }
    
    public async Task CreateCategory(CategoryDto categoryDto)
    {
        await _categoryProvider.Create(new CategoryEntity
        {
            Name = categoryDto.Name,
        });
    }

    public async Task UpdateCategory(CategoryDto categoryDto)
    {
        var categoryEntity = await _categoryProvider.GetById(categoryDto.Id);
        categoryEntity.Name = categoryDto.Name;
        await _categoryProvider.Update(categoryEntity);
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        var fromDb = await _categoryProvider.GetAll();
        return fromDb.Select(x => new CategoryDto(x.Id, x.Name)).ToList();
    }
}