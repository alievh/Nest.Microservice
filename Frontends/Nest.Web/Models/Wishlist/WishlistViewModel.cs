namespace Nest.Web.Models.Wishlist;

public class WishlistViewModel
{
    public WishlistViewModel()
    {
        WishlistItems = new List<WishlistItemViewModel>();
    }

    public string UserId { get; set; }

    public List<WishlistItemViewModel> WishlistItems { get; set; }

}
