namespace Nest.Services.BasketApi.DTO_s;

public class WishlistItemDto
{
    public int Quantity { get; set; }

    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public List<string>? Pictures { get; set; }

    public decimal Price { get; set; }
}
