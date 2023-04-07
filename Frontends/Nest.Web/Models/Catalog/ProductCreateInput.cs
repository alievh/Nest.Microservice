using System.ComponentModel.DataAnnotations;

namespace Nest.Web.Models.Catalog;

public class ProductCreateInput
{
    [Display(Name = "Product's Name")]
    public string Name { get; set; }

    [Display(Name = "Product's Description")]
    public string Description { get; set; }

    [Display(Name = "Product's Price")]
    public decimal Price { get; set; }

    [Display(Name = "Product's Stock")]
    public int Stock { get; set; }

    public string? UserId { get; set; }
    public List<string>? Pictures { get; set; }


    [Display(Name = "Product's Category")]
    public string SubCategoryId { get; set; }

    [Display(Name = "Product's Photo")]
    public List<IFormFile>? PhotoFormFiles { get; set; }
}
