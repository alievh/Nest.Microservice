using Nest.Web.Models.Catalog;

namespace Nest.Web.Services.Interfaces;

public interface ICatalogService
{
    Task<List<ProductViewModel>> GetAllProductAsync();
    Task<List<CategoryViewModel>> GetAllCategoryAsync();
    Task<List<SubCategoryViewModel>> GetAllSubCategoryAsync();
    Task<List<ProductViewModel>> GetAllProductByUserIdAsync(string userId);
    Task<List<ProductViewModel>> GetAllProductBySubCategoryAsync(string subCategoryId);
    Task<ProductViewModel> GetProductByIdAsync(string productId);
    Task<bool> CreateProductAsync(ProductCreateInput productCreateInput);
    Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput);
    Task<bool> DeleteProductAsync(string productId);
}
