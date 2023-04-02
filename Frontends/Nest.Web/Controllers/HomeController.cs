using Microsoft.AspNetCore.Mvc;

namespace Nest.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}