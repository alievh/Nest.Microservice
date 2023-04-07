using Microsoft.AspNetCore.Mvc;
using Nest.Web.Models.Wishlist;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Controllers;

public class WishlistController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly IWishlistService _wishlistService;

    public WishlistController(ICatalogService catalogService, IWishlistService wishlistService)
    {
        _catalogService = catalogService;
        _wishlistService = wishlistService;
    }
    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        return View(await _wishlistService.Get());
    }

    public async Task<IActionResult> AddWishlistItem(string id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        var product = await _catalogService.GetProductByIdAsync(id);

        var wishlistItemViewModel = new WishlistItemViewModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = 1,
            Pictures = product.StockPictureUrls
        };

        await _wishlistService.AddWishlistItem(wishlistItemViewModel);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveWishlistItem(string id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        await _wishlistService.RemoveWishlistItem(id);

        return RedirectToAction(nameof(Index));
    }
}
