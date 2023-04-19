using Microsoft.AspNetCore.Mvc;
using VirtuMerce.Api.ViewModels;
using VitruMerce.Bll;
using VitruMerce.Bll.Dtos;

namespace VirtuMerce.Api.Controllers;

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("api/products/")]
    public async Task<IActionResult> CreateProduct(CreateProductViewModel createProductViewModel)
    {
        await _productService.CreateProduct(new ProductDto(Guid.NewGuid(), createProductViewModel.Title,
            createProductViewModel.Details, createProductViewModel.Price));
        return Ok();
    }

    [HttpDelete("api/products/")]
    public async Task<IActionResult> DeleteProduct(IdProductViewModel idProductViewModel)
    {
        await _productService.DeleteProduct(idProductViewModel.Id);
        return Ok();
    }

    [HttpGet("api/products/{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product=await _productService.GetProductById(id);
        return Ok(product);
    }

    [HttpPut("api/products/{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CreateProductViewModel createProductViewModel)
    {
        await _productService.UpdateProduct(new ProductDto(id,createProductViewModel.Title, createProductViewModel.Details,
            createProductViewModel.Price));
        return Ok();
    }

    [HttpGet("api/products")]
    public async Task<IActionResult> GetAll()
    {
        var products=await _productService.GetAll();
        return Ok(products);
    }

}