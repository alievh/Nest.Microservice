namespace Nest.Web.Models.Order;

public class OrderViewModel
{
    public int? Id { get; set; }
    public DateTime CreatedDate { get; set; }

    // public Address Address { get; set; }

    public string? BuyerId { get; set; }

    public AddressCreateInput Address { get; set; }

    public List<OrderItemViewModel>? OrderItems { get; set; }

    public decimal TotalPrice { get => OrderItems.Sum(x => (decimal)x.Price * x.Quantity); }
}
