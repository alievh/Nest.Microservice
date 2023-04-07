namespace Nest.Web.Models.Catalog;

public class SubCategoryViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedTime { get; set; }
    public string CategoryId { get; set; } = null!;
    public CategoryViewModel? Category { get; set; }
}
