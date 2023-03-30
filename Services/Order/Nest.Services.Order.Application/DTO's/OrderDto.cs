namespace Nest.Services.Order.Application.DTO_s;

public class OrderDto
{
    public int Id { get; set; };
    public DateTime? CreatedDate { get; set; }
    public AddressDto? Address { get; set; }
    public string BuyerId { get; set; } = null!;

    public List<OrderItemDto>? OrderItems { get; set; }
}
