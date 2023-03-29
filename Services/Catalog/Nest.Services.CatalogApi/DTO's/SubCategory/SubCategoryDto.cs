using Nest.Services.CatalogApi.DTO_s.Category;

namespace Nest.Services.CatalogApi.DTO_s.SubCategory;

public class SubCategoryDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public CategoryDto? Category { get; set; }
}
