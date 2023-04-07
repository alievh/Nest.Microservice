namespace Nest.Services.CatalogApi.DTO_s.Product;

public class ProductCreateDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }
    public int Stock { get; set; }

    public string? UserId { get; set; }
    public List<string> Pictures { get; set; } = null!;

    public string SubCategoryId { get; set; } = null!;

}
