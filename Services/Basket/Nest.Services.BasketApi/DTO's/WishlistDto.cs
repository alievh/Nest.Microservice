namespace Nest.Services.BasketApi.DTO_s;

public class WishlistDto
{
    public string? UserId { get; set; }

    public List<WishlistItemDto>? WishlistItems { get; set; }
}
