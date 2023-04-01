namespace Nest.Services.PaymentApi.DTO_s;

public class PaymentDto
{
    public string CardName { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public string Expiration { get; set; } = null!;
    public string CVV { get; set; } = null!;
    public decimal TotalPrice { get; set; }

    public OrderDto? Order { get; set; }
}
