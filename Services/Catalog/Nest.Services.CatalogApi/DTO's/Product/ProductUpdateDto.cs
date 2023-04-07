namespace Nest.Services.CatalogApi.DTO_s.Product;

public class ProductUpdateDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int? Sold { get; set; }

    public string? UserId { get; set; }
    public List<string>? Pictures { get; set; }

    public string SubCategoryId { get; set; }

}
