using Microsoft.AspNetCore.Mvc;

namespace Nest.Web.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
