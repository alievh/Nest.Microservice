using System.ComponentModel.DataAnnotations;

namespace Nest.Web.Models.Order;

public class CheckoutInfoInput
{
    [Display(Name = "Province")]
    public string? Province { get; set; }

    [Display(Name = "District")]
    public string? District { get; set; }

    [Display(Name = "Street")]
    public string? Street { get; set; }

    [Display(Name = "Zip code")]
    public string? ZipCode { get; set; }

    [Display(Name = "Line")]
    public string? Line { get; set; }

    [Display(Name = "Card name and surname")]
    public string? CardName { get; set; }

    [Display(Name = "Card number")]
    public string? CardNumber { get; set; }

    [Display(Name = "Card expiration(mm/yy)")]
    public string? Expiration { get; set; }

    [Display(Name = "CVV/CVV2")]
    public string? CVV { get; set; }
    public decimal? TotalPrice { get; set; }
}
