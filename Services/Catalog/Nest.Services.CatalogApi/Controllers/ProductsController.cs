using Microsoft.AspNetCore.Mvc;
using Nest.Services.CatalogApi.DTO_s.Product;
using Nest.Services.CatalogApi.Services.Interfaces;
using Nest.Shared.ControllerBases;

namespace Nest.Services.CatalogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : CustomBaseController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _productService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _productService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet]
    [Route("/api/[controller]/GetAllByUserId/{userId}")]
    public async Task<IActionResult> GetAllByUserId(string userId)
    {
        var response = await _productService.GetByUserIdAsync(userId);
        return CreateActionResultInstance(response);
    }

    [HttpGet]
    [Route("/api/[controller]/GetAllBySubCategory/{subCategoryId}")]
    public async Task<IActionResult> GetAllBySubCategory(string subCategoryId)
    {
        var response = await _productService.GetBySubCategoryAsync(subCategoryId);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
    {
        var result = await _productService.CreateAsync(productCreateDto);
        return CreateActionResultInstance(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
        var result = await _productService.UpdateAsync(productUpdateDto);
        return CreateActionResultInstance(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _productService.DeleteAsync(id);
        return CreateActionResultInstance(result);
    }
}
