using Microsoft.AspNetCore.Mvc;
using Nest.Web.Models.Order;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var basket = await _basketService.Get();
            ViewBag.basket = basket;

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }
            // SYNC
            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            // ASYNC
            var orderStatus = await _orderService.SuspendOrder(checkoutInfoInput);

            if (orderStatus.IsSuccessfull == false)
            {
                var basket = await _basketService.Get();

                ViewBag.basket = basket;

                ViewBag.error = orderStatus.Error;

                return View();
            }

            ViewBag.Basket = await _basketService.Get();
            // SYNC
            //return RedirectToAction(nameof(SuccesfulCheckout), new { orderId = orderStatus.OrderId });

            // ASYNC
            return RedirectToAction(nameof(SuccessfullCheckout));
        }

        public async Task<IActionResult> SuccessfullCheckout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var orders = await _orderService.GetOrder();

            return View(orders.OrderByDescending(x => x.CreatedDate).FirstOrDefault());
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            return View(await _orderService.GetOrder());
        }
    }
}
