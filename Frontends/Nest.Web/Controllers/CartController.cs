using Microsoft.AspNetCore.Mvc;

namespace Nest.Web.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
