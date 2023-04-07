using Nest.Shared.DTO_s;
using Nest.Shared.Services;
using Nest.Web.Models.FakePayment;
using Nest.Web.Models.Order;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class OrderService : IOrderService
{
    private readonly IPaymentService _paymentService;
    private readonly HttpClient _httpClient;
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _identitService;

    public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService identitService)
    {
        _paymentService = paymentService;
        _httpClient = httpClient;
        _basketService = basketService;
        _identitService = identitService;
    }

    public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
    {
        var basket = await _basketService.Get();

        var paymentInfoInput = new PaymentInfoInput()
        {
            CardName = checkoutInfoInput.CardName,
            CardNumber = checkoutInfoInput.CardNumber,
            Expiration = checkoutInfoInput.Expiration,
            CVV = checkoutInfoInput.CVV,
            TotalPrice = basket.TotalPrice
        };

        var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

        if (!responsePayment)
        {
            return new OrderCreatedViewModel() { Error = "Payment not successfull", IsSuccessfull = false };
        }

        var orderCreateInput = new OrderCreateInput()
        {
            BuyerId = _identitService.GetUserId,
            Address = new AddressCreateInput()
            {
                Province = checkoutInfoInput.Province,
                District = checkoutInfoInput.District,
                Street = checkoutInfoInput.Street,
                Line = checkoutInfoInput.Line,
                ZipCode = checkoutInfoInput.ZipCode
            }
        };

        basket.BasketItems.ForEach(x =>
        {
            var orderItem = new OrderItemCreateInput()
            {
                ProductId = x.ProductId,
                Price = x.GetCurrentPrice,
                PictureUrl = "",
                ProductName = x.ProductName,
                Quantity = x.Quantity
            };
            orderCreateInput.OrderItems.Add(orderItem);
        });

        var response = await _httpClient.PostAsJsonAsync("orders", orderCreateInput);

        if (!response.IsSuccessStatusCode)
        {
            return new OrderCreatedViewModel() { Error = "Order can not created", IsSuccessfull = false };
        }
        var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<ResponseDto<OrderCreatedViewModel>>();

        orderCreatedViewModel.Data.IsSuccessfull = true;

        await _basketService.Delete();

        return orderCreatedViewModel.Data;
    }

    public async Task<List<OrderViewModel>> GetOrder()
    {
        var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<OrderViewModel>>>("orders");

        return response.Data;
    }

    public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
    {
        var basket = await _basketService.Get();

        var orderCreateInput = new OrderCreateInput()
        {
            BuyerId = _identitService.GetUserId,
            Address = new AddressCreateInput()
            {
                Province = checkoutInfoInput.Province,
                District = checkoutInfoInput.District,
                Street = checkoutInfoInput.Street,
                Line = checkoutInfoInput.Line,
                ZipCode = checkoutInfoInput.ZipCode
            }
        };

        basket.BasketItems.ForEach(x =>
        {
            var orderItem = new OrderItemCreateInput()
            {
                ProductId = x.ProductId,
                Price = x.GetCurrentPrice,
                PictureUrl = "",
                ProductName = x.ProductName,
                Quantity = x.Quantity
            };
            orderCreateInput.OrderItems.Add(orderItem);
        });

        var paymentInfoInput = new PaymentInfoInput()
        {
            CardName = checkoutInfoInput.CardName,
            CardNumber = checkoutInfoInput.CardNumber,
            Expiration = checkoutInfoInput.Expiration,
            CVV = checkoutInfoInput.CVV,
            TotalPrice = basket.TotalPrice,
            Order = orderCreateInput,
        };

        var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

        if (!responsePayment)
        {
            return new OrderSuspendViewModel() { Error = "Payment not successfull", IsSuccessfull = false };
        }

        await _basketService.Delete();
        return new OrderSuspendViewModel() { IsSuccessfull = true };
    }
}
