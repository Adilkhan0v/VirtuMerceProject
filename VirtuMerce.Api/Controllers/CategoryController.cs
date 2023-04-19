using Microsoft.AspNetCore.Mvc;
using VirtuMerce.Api.ViewModels;
using VitruMerce.Bll;
using VitruMerce.Bll.Dtos;

namespace VirtuMerce.Api.Controllers;

public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("api/categories/")]
    public async Task<IActionResult> CreateCategory(CreateCategoryViewModel createCategoryViewModel)
    { 
        await _categoryService.CreateCategory(new CategoryDto(Guid.NewGuid(),createCategoryViewModel.Name));
        return Ok();
    }

    [HttpDelete("api/categories/")]
    public async Task<IActionResult> DeleteCategory(IdCategoryViewModel idCategoryViewModel)
    {
        await _categoryService.DeleteCategory(idCategoryViewModel.Id);
        return Ok();
    }

    [HttpGet("api/category/{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category=await _categoryService.GetCategoryById(id);
        return Ok(category);
    }

    [HttpPut("api/categories/{id:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CreateCategoryViewModel createCategoryViewModel)
    {
        await _categoryService.UpdateCategory(new CategoryDto(id, createCategoryViewModel.Name));
        return Ok();
    }

    [HttpGet("api/categories")]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAll();
        return Ok(categories);
    }
    
}