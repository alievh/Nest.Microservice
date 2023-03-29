using Nest.Services.CatalogApi.DTO_s.SubCategory;
using Nest.Shared.DTO_s;

namespace Nest.Services.CatalogApi.Services.Interfaces;

public interface ISubCategoryService
{
    Task<ResponseDto<List<SubCategoryDto>>> GetAllAsync();
    Task<ResponseDto<SubCategoryDto>> CreateAsync(SubCategoryDto subCategoryDto);
    Task<ResponseDto<SubCategoryDto>> GetByIdAsync(string id);
}
