using Microsoft.AspNetCore.Mvc;

namespace Nest.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction(nameof(Login));
        }
    }
}
