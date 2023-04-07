using Nest.Shared.DTO_s;
using Nest.Web.Models.Basket;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;
    private readonly IDiscountService _discountService;

    public BasketService(HttpClient httpClient, IDiscountService discountService)
    {
        _httpClient = httpClient;
        _discountService = discountService;
    }

    public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
    {
        var basket = await Get();

        if (basket != null)
        {
            if (!basket.BasketItems.Any(x => x.ProductId == basketItemViewModel.ProductId))
            {
                basketItemViewModel.Quantity = 1;
                basket.BasketItems.Add(basketItemViewModel);
            }
            else
            {
                var basketItem = basket.BasketItems.Where(x => x.ProductId == basketItemViewModel?.ProductId).FirstOrDefault();
                basketItem.Quantity++;
            }
        }
        else
        {
            basket = new BasketViewModel();
            basketItemViewModel.Quantity = 1;
            basket.BasketItems.Add(basketItemViewModel);
        }

        await SaveOrUpdate(basket);
    }

    public async Task IncreaseQuantity(string id)
    {
        var basket = await Get();
        if (basket != null)
        {
            if (basket.BasketItems.Any(x => x.ProductId == id))
            {
                basket.BasketItems.Where(x => x.ProductId == id).FirstOrDefault().Quantity++;
            }
        }

        await SaveOrUpdate(basket);
    }

    public async Task DecreaseQuantity(string id)
    {
        var basket = await Get();
        if (basket != null)
        {
            if (basket.BasketItems.Any(x => x.ProductId == id))
            {
                if (basket.BasketItems.Where(x => x.ProductId == id).FirstOrDefault().Quantity != 1)
                {
                    basket.BasketItems.Where(x => x.ProductId == id).FirstOrDefault().Quantity--;
                }
            }
        }

        await SaveOrUpdate(basket);
    }

    public async Task<bool> ApplyDiscount(string discountCode)
    {
        await CancelApplyDiscount();

        var basket = await Get();
        if (basket == null) return false;

        var hasDiscount = await _discountService.GetDiscount(discountCode);
        if (hasDiscount == null) return false;

        basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);
        await SaveOrUpdate(basket);

        return true;
    }

    public async Task<bool> CancelApplyDiscount()
    {
        var basket = await Get();

        if (basket == null || basket.DiscountCode == null) return false;

        basket.CancelDiscount();
        await SaveOrUpdate(basket);

        return true;
    }

    public async Task<bool> Delete()
    {
        var result = await _httpClient.DeleteAsync("basket");

        return result.IsSuccessStatusCode;
    }

    public async Task<BasketViewModel> Get()
    {
        var response = await _httpClient.GetAsync("basket");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var basketViewModel = await response.Content.ReadFromJsonAsync<ResponseDto<BasketViewModel>>();
        return basketViewModel.Data;
    }

    public async Task<bool> RemoveBasketItem(string productId)
    {
        var basket = await Get();
        if (basket == null) return false;

        var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
        if (deleteBasketItem == null) return false;

        var deleteResult = basket.BasketItems.Remove(deleteBasketItem);
        if (!deleteResult) return false;

        if (!basket.BasketItems.Any()) basket.DiscountCode = null;

        return await SaveOrUpdate(basket);
    }

    public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("basket", basketViewModel);

        return response.IsSuccessStatusCode;
    }
}
