using Nest.Services.CatalogApi.DTO_s.Product;
using Nest.Shared.DTO_s;

namespace Nest.Services.CatalogApi.Services.Interfaces;

public interface IProductService
{
    Task<ResponseDto<List<ProductDto>>> GetAllAsync();
    Task<ResponseDto<ProductDto>> GetByIdAsync(string id);
    Task<ResponseDto<List<ProductDto>>> GetByUserIdAsync(string userId);
    Task<ResponseDto<List<ProductDto>>> GetBySubCategoryAsync(string subCategoryId);
    Task<ResponseDto<ProductDto>> CreateAsync(ProductCreateDto productCreateDto);
    Task<ResponseDto<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto);
    Task<ResponseDto<NoContent>> DeleteAsync(string id);
}
