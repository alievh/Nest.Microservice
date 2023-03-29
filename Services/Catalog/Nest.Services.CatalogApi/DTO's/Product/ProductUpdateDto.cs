using Nest.Services.CatalogApi.DTO_s.SubCategory;

namespace Nest.Services.CatalogApi.DTO_s.Product;

public class ProductUpdateDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string? UserId { get; set; }
    public List<string> Picture { get; set; } = null!;

    public List<SubCategoryDto> SubCategories { get; set; } = null!;
}
