using Microsoft.AspNetCore.Mvc;

namespace Nest.Web.Controllers;

public class WishlistController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
