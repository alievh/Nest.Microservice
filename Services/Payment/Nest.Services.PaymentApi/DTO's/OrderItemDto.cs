namespace Nest.Services.PaymentApi.DTO_s;

public class OrderItemDto
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
