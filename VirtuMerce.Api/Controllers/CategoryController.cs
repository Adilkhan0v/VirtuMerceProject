using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using VirtuMerce.Api.ViewModels;
using VitruMerce.Bll;
using VitruMerce.Bll.Dtos;

namespace VirtuMerce.Api.Controllers;

[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IAuthService _authService;

    public CategoryController(ICategoryService categoryService, IAuthService authService)
    {
        _categoryService = categoryService;
        _authService = authService;
    }

    [HttpPost("api/categories/")]
    public async Task<IActionResult> CreateCategory(CreateCategoryViewModel createCategoryViewModel)
    {
        var headers = Request.Headers[HeaderNames.Authorization].ToArray();
        var currentUser = await _authService.GetUserByHeaders(headers!);
        
        var id = await _categoryService.CreateCategory(new CategoryDto(Guid.NewGuid(),createCategoryViewModel.Name, currentUser.Id));
        return Ok(id);
    }

    [HttpDelete("api/categories/{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var headers = Request.Headers[HeaderNames.Authorization].ToArray();
        var currentUser = await _authService.GetUserByHeaders(headers!);
        
        await _categoryService.DeleteCategory(id, currentUser.Id);
        return Ok();
    }

    [HttpGet("api/category/{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var headers = Request.Headers[HeaderNames.Authorization].ToArray();
        var currenUser = await _authService.GetUserByHeaders(headers!);
        
        var category=await _categoryService.GetCategoryById(id, currenUser.Id);
        return Ok(category);
    }

    [HttpPut("api/categories/{id:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CreateCategoryViewModel createCategoryViewModel)
    {
        var headers = Request.Headers[HeaderNames.Authorization].ToArray();
        var currentUser = await _authService.GetUserByHeaders(headers!);
        
        await _categoryService.UpdateCategory(new CategoryDto(id, createCategoryViewModel.Name, currentUser.Id));
        return Ok();
    }

    [HttpGet("api/categories")]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAll();
        return Ok(categories);
    }
    
}