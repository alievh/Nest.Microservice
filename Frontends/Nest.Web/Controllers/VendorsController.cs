using Microsoft.AspNetCore.Mvc;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Controllers;

public class VendorsController : Controller
{
    private readonly IVendorService _vendorService;

    public VendorsController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _vendorService.GetVendors());
    }
}
