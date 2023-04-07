using Microsoft.AspNetCore.Mvc;
using Nest.Web.Services.Interfaces;
using Nest.Web.ViewModels;

namespace Nest.Web.Controllers;

public class ShopController : Controller
{
    private readonly ICatalogService _catalogService;

    public ShopController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index(string subCategoryId)
    {
        ShopVM shopVm = new();

        if (subCategoryId == null)
        {
            shopVm.Products = await _catalogService.GetAllProductAsync();
        }
        else
        {
            shopVm.Products = await _catalogService.GetAllProductBySubCategoryAsync(subCategoryId);
        }
        shopVm.SubCategories = await _catalogService.GetAllSubCategoryAsync();

        return View(shopVm);
    }

    public async Task<IActionResult> Detail(string id)
    {
        return View(await _catalogService.GetProductByIdAsync(id));
    }
}
