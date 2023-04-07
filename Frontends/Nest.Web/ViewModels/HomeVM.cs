using Nest.Web.Models.Catalog;

namespace Nest.Web.ViewModels;

public class HomeVM
{
    public List<ProductViewModel> Products { get; set; }
    public List<SubCategoryViewModel> SubCategories { get; set; }
    public List<CategoryViewModel> Categories { get; set; }
}
