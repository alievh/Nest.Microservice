using Nest.Web.Models.Wishlist;

namespace Nest.Web.Services.Interfaces;

public interface IWishlistService
{
    Task<bool> SaveOrUpdate(WishlistViewModel wishlistViewModel);
    Task<WishlistViewModel> Get();
    Task<bool> Delete();
    Task AddWishlistItem(WishlistItemViewModel wishlistItemViewModel);
    Task<bool> RemoveWishlistItem(string productId);
}
