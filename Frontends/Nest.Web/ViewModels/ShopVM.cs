using Nest.Web.Models.Catalog;

namespace Nest.Web.ViewModels;

public class ShopVM
{
    public List<ProductViewModel> Products { get; set; }
    public List<SubCategoryViewModel> SubCategories { get; set; }
}
