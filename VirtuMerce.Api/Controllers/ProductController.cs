using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using VirtuMerce.Api.ViewModels;
using VitruMerce.Bll;
using VitruMerce.Bll.Dtos;

namespace VirtuMerce.Api.Controllers;

[Authorize]

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IAuthService _authService;

    public ProductController(IProductService productService, IAuthService authService)
    {
        _productService = productService;
        _authService = authService;
    }
    
    [HttpPost("api/products/")]
    public async Task<IActionResult> CreateProduct(CreateProductViewModel createProductViewModel)
    {

        var headers = Request.Headers[HeaderNames.Authorization].ToArray();
        
        var currentUser = await _authService.GetUserByHeaders(headers!);

        var id = await _productService.CreateProduct(new ProductDto(Guid.NewGuid(), createProductViewModel.Title,
            createProductViewModel.Details, createProductViewModel.Price, currentUser.Id));
        return Ok(id);
        
    }

    [HttpDelete("api/products/{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var headers = Request.Headers[HeaderNames.Authorization].ToArray();

        var currentUser = await _authService.GetUserByHeaders(headers!);
        
        await _productService.DeleteProduct(id, currentUser.Id);
        return Ok();
    }

    [HttpGet("api/products/{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var headers = Request.Headers[HeaderNames.Authorization].ToArray();
        var currentUser = await _authService.GetUserByHeaders(headers!);
        
        var product=await _productService.GetProductById(id,currentUser.Id);
        return Ok(product);
    }

    [HttpPut("api/products/{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CreateProductViewModel createProductViewModel)
    {
        var headers = Request.Headers[HeaderNames.Authorization].ToArray();

        var currentUser = await _authService.GetUserByHeaders(headers!);
        
        await _productService.UpdateProduct(new ProductDto(id,createProductViewModel.Title, createProductViewModel.Details,
            createProductViewModel.Price, currentUser.Id));
        return Ok();
    }

    [HttpGet("api/products")]
    public async Task<IActionResult> GetAll()
    {
        var products=await _productService.GetAll();
        return Ok(products);
    }

}