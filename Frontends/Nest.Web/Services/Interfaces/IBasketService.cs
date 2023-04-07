using Nest.Web.Models.Basket;

namespace Nest.Web.Services.Interfaces;

public interface IBasketService
{
    Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);
    Task<BasketViewModel> Get();
    Task<bool> Delete();
    Task AddBasketItem(BasketItemViewModel basketItemViewModel);
    Task IncreaseQuantity(string id);
    Task DecreaseQuantity(string id);
    Task<bool> RemoveBasketItem(string productId);
    Task<bool> ApplyDiscount(string discountCode);
    Task<bool> CancelApplyDiscount();
}
