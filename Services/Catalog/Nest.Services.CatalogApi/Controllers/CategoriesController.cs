using Microsoft.AspNetCore.Mvc;
using Nest.Services.CatalogApi.DTO_s.Category;
using Nest.Services.CatalogApi.Services.Interfaces;
using Nest.Shared.ControllerBases;

namespace Nest.Services.CatalogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : CustomBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return CreateActionResultInstance(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return CreateActionResultInstance(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto categoryDto)
    {
        var result = await _categoryService.CreateAsync(categoryDto);
        return CreateActionResultInstance(result);
    }
}
