using Microsoft.AspNetCore.Mvc;
using Nest.Services.CatalogApi.DTO_s.SubCategory;
using Nest.Services.CatalogApi.Services.Interfaces;
using Nest.Shared.ControllerBases;

namespace Nest.Services.CatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : CustomBaseController
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subCategories = await _subCategoryService.GetAllAsync();
            return CreateActionResultInstance(subCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var subCategories = await _subCategoryService.GetByIdAsync(id);
            return CreateActionResultInstance(subCategories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryDto subCategoryDto)
        {
            var result = await _subCategoryService.CreateAsync(subCategoryDto);
            return CreateActionResultInstance(result);
        }
    }
}
