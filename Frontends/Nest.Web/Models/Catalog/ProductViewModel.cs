namespace Nest.Web.Models.Catalog;

public class ProductViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get => Description.Length > 100 ? Description[..100] + "..." : Description; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
    public int? Sold { get; set; }

    public string UserId { get; set; }
    public List<string> Pictures { get; set; }
    public List<string> StockPictureUrls { get; set; }

    public DateTime CreatedTime { get; set; }

    public string SubCategoryId { get; set; }

    public SubCategoryViewModel SubCategory { get; set; }
}
