using Microsoft.AspNetCore.Mvc;

namespace Nest.Web.Controllers;

public class ShopController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Detail()
    {
        return View();
    }
}
