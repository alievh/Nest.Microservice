using Microsoft.AspNetCore.Mvc;

namespace Nest.Web.Controllers;
public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
