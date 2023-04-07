using Microsoft.AspNetCore.Mvc;
using Nest.Web.Services.Interfaces;
using Nest.Web.ViewModels;

namespace Nest.Web.Controllers;
public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;

    public AccountController(IUserService userService, IOrderService orderService)
    {
        _userService = userService;
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Authentication");
        }

        AccountVM accountVm = new()
        {
            User = await _userService.GetUser(),
            Orders = await _orderService.GetOrder(),
        };

        return View(accountVm);
    }


}
