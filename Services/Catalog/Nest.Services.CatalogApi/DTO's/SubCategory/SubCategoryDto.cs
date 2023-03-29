using Nest.Services.CatalogApi.DTO_s.Category;

namespace Nest.Services.CatalogApi.DTO_s.SubCategory;

public class SubCategoryDto
{
    public string CategoryId { get; set; } = null!;
    public CategoryDto? Category { get; set; }
}
