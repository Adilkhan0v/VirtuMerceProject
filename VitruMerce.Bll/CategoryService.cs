using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VitruMerce.Bll.Dtos;

namespace VitruMerce.Bll;

public class CategoryService : ICategoryService
{
    private readonly ICategoryProvider _categoryProvider;
    private readonly IUserProvider _userProvider;

    public CategoryService(ICategoryProvider categoryProvider, IUserProvider userProvider)
    {
        _categoryProvider = categoryProvider;
        _userProvider = userProvider;
    }
    
    public async Task<CategoryDto> GetCategoryById(Guid id, Guid userId)
    {
        var category = await _categoryProvider.GetById(id);

        if (category.User.Id != userId)
        {
            throw new ArgumentException("Log in first!");
        }

        return new CategoryDto(category.Id, category.Name, category.User.Id);
    }
    

    public async Task DeleteCategory(Guid id, Guid userId)
    {
        var categoryEntity = await _categoryProvider.GetById(id);
        if (categoryEntity.User.Id != userId)
        {
            throw new ArgumentException("Log in first!");
        }
        await _categoryProvider.Delete(id);
    }
    
    public async Task<Guid> CreateCategory(CategoryDto categoryDto)
    {
        var user = await _userProvider.GetById(categoryDto.UserId);
        
        await _categoryProvider.Create(new CategoryEntity
        {
            Name = categoryDto.Name,
            User = user
        });
        return categoryDto.Id;
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
        return fromDb.Select(x => new CategoryDto(x.Id, x.Name, x.User.Id)).ToList();
    }
}