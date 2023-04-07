using Nest.Web.Models.Discount;

namespace Nest.Web.Services.Interfaces;

public interface IDiscountService
{
    Task<DiscountViewModel> GetDiscount(string discountCode);
}
