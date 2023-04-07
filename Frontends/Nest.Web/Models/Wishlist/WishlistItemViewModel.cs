namespace Nest.Web.Models.Wishlist;

public class WishlistItemViewModel
{
    public int Quantity { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }

    public List<string>? Pictures { get; set; }

    public decimal Price { get; set; }
}
