using Nest.Shared.DTO_s;
using Nest.Web.Models.Wishlist;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class WishlistService : IWishlistService
{
    private readonly HttpClient _httpClient;

    public WishlistService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddWishlistItem(WishlistItemViewModel wishlistItemViewModel)
    {
        var wishlist = await Get();

        if (wishlist != null)
        {
            if (!wishlist.WishlistItems.Any(x => x.ProductId == wishlistItemViewModel.ProductId))
            {
                wishlist.WishlistItems.Add(wishlistItemViewModel);
            }
        }
        else
        {
            wishlist = new WishlistViewModel();
            wishlist.WishlistItems.Add(wishlistItemViewModel);
        }

        await SaveOrUpdate(wishlist);
    }

    public async Task<bool> Delete()
    {
        var result = await _httpClient.DeleteAsync("wishlist");

        return result.IsSuccessStatusCode;
    }

    public async Task<WishlistViewModel> Get()
    {
        var response = await _httpClient.GetAsync("wishlist");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var wishlistViewModel = await response.Content.ReadFromJsonAsync<ResponseDto<WishlistViewModel>>();
        return wishlistViewModel.Data;
    }

    public async Task<bool> RemoveWishlistItem(string productId)
    {
        var wishlist = await Get();
        if (wishlist == null) return false;

        var deleteWishlistItem = wishlist.WishlistItems.FirstOrDefault(x => x.ProductId == productId);
        if (deleteWishlistItem == null) return false;

        var deleteResult = wishlist.WishlistItems.Remove(deleteWishlistItem);
        if (!deleteResult) return false;

        return await SaveOrUpdate(wishlist);
    }

    public async Task<bool> SaveOrUpdate(WishlistViewModel wishlistViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("wishlist", wishlistViewModel);

        return response.IsSuccessStatusCode;
    }
}
