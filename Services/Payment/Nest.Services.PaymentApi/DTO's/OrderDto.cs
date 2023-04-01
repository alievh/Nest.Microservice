namespace Nest.Services.PaymentApi.DTO_s;

public class OrderDto
{
    public OrderDto()
    {
        OrderItems = new List<OrderItemDto>();
    }

    public string BuyerId { get; set; } = null!;
    public List<OrderItemDto>? OrderItems { get; set; }
    public AddressDto? Address { get; set; }
}
