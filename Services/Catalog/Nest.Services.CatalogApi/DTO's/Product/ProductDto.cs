using Nest.Services.CatalogApi.DTO_s.SubCategory;

namespace Nest.Services.CatalogApi.DTO_s.Product;

public class ProductDto
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string UserId { get; set; } = null!;
    public List<string> Pictures { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public List<SubCategoryDto>? SubCategories { get; set; }
}
