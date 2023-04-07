namespace Nest.Web.Models.Basket;

public class BasketItemViewModel
{
    public int Quantity { get; set; }

    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public List<string>? Pictures { get; set; }

    public decimal Price { get; set; }
    private decimal? DiscountAppliedPrice;


    public decimal GetCurrentPrice
    {
        get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
    }
    public decimal TotoalPrice
    {
        get => GetCurrentPrice * Quantity;
    }
    public void AppliedDiscount(decimal discountPrice)
    {
        DiscountAppliedPrice = discountPrice;
    }
}
