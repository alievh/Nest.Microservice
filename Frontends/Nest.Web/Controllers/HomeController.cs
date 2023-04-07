using Microsoft.AspNetCore.Mvc;
using Nest.Web.Services.Interfaces;
using Nest.Web.ViewModels;

namespace Nest.Web.Controllers;

public class HomeController : Controller
{
    private readonly ICatalogService _catalogService;

    public HomeController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index()
    {
        HomeVM homeVM = new()
        {
            Products = await _catalogService.GetAllProductAsync(),
            SubCategories = await _catalogService.GetAllSubCategoryAsync(),
            Categories = await _catalogService.GetAllCategoryAsync()
        };

        return View(homeVM);
    }
}