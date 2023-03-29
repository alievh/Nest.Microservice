using Nest.Services.CatalogApi.DTO_s.Category;
using Nest.Shared.DTO_s;

namespace Nest.Services.CatalogApi.Services.Interfaces;

public interface ICategoryService
{
    Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
    Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto);
    Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
}
