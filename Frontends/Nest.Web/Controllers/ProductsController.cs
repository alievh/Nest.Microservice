using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nest.Shared.Services;
using Nest.Web.Models.Catalog;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public ProductsController(ISharedIdentityService sharedIdentityService, ICatalogService catalogService)
        {
            _sharedIdentityService = sharedIdentityService;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            return View(await _catalogService.GetAllProductByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var categories = await _catalogService.GetAllSubCategoryAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInput productCreateInput)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var categories = await _catalogService.GetAllCategoryAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            if (!ModelState.IsValid) return View();

            productCreateInput.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.CreateProductAsync(productCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var product = await _catalogService.GetProductByIdAsync(id);

            var categories = await _catalogService.GetAllSubCategoryAsync();

            if (product == null)
            {
                //Mesaj goster
                RedirectToAction(nameof(Index));
            }

            ViewBag.categoryList = new SelectList(categories, "Id", "Name", product.Id);
            ProductUpdateInput productUpdateInput = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                SubCategoryId = product.SubCategoryId,
                UserId = product.UserId,
                Pictures = product.Pictures,
                Sold = product.Sold
            };

            return View(productUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateInput productUpdateInput)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", productUpdateInput.Id);

            if (!ModelState.IsValid) return View();

            await _catalogService.UpdateProductAsync(productUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            await _catalogService.DeleteProductAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
