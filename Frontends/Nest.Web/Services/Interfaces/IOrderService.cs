using Nest.Web.Models.Order;

namespace Nest.Web.Services.Interfaces;

public interface IOrderService
{
    /// <summary>
    /// Sync connection
    /// </summary>
    /// <param name="checkoutInfoInput"></param>
    /// <returns></returns>
    Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);

    /// <summary>
    /// Async connection - RabbitMQ
    /// </summary>
    /// <param name="checkoutInfoInput"></param>
    /// <returns></returns>
    Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);
    Task<List<OrderViewModel>> GetOrder();
}
