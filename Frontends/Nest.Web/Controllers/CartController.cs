using Microsoft.AspNetCore.Mvc;
using Nest.Web.Models.Basket;
using Nest.Web.Models.Discount;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Controllers;

public class CartController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;

    public CartController(ICatalogService catalogService, IBasketService basketService)
    {
        _catalogService = catalogService;
        _basketService = basketService;
    }
    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        return View(await _basketService.Get());
    }

    public async Task<IActionResult> AddBasketItem(string id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        var course = await _catalogService.GetProductByIdAsync(id);

        var basketItemViewModel = new BasketItemViewModel
        {
            ProductId = course.Id,
            ProductName = course.Name,
            Price = course.Price,
            Pictures = course.StockPictureUrls
        };

        await _basketService.AddBasketItem(basketItemViewModel);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> IncreaseQuantity(string id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        await _basketService.IncreaseQuantity(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DecreaseQuantity(string id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        await _basketService.DecreaseQuantity(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveBasketItem(string id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        await _basketService.RemoveBasketItem(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ApplyDiscount(DiscountApplyInput discountApplyInput)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        if (!ModelState.IsValid)
        {
            TempData["discountError"] = ModelState.Values.SelectMany(x => x.Errors).First().ErrorMessage;
            return RedirectToAction(nameof(Index));
        }

        var discountStatus = await _basketService.ApplyDiscount(discountApplyInput.Code);

        TempData["discountStatus"] = discountStatus;

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> CancelApplyDiscount()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        await _basketService.CancelApplyDiscount();

        return RedirectToAction(nameof(Index));
    }
}
