using System.ComponentModel.DataAnnotations;

namespace Nest.Web.Models.Catalog;

public class ProductUpdateInput
{
    public string Id { get; set; } = null!;

    [Display(Name = "Product's Name")]
    public string Name { get; set; } = null!;

    [Display(Name = "Product's Description")]
    public string Description { get; set; } = null!;

    [Display(Name = "Product's Price")]
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int? Sold { get; set; }
    public string? UserId { get; set; }
    public List<string>? Pictures { get; set; }


    [Display(Name = "Product's category")]
    public string SubCategoryId { get; set; }

    [Display(Name = "Product's photo")]
    public List<IFormFile>? PhotoFormFiles { get; set; }
}
