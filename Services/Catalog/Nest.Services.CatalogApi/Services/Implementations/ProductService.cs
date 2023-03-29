using Nest.Services.CatalogApi.DTO_s.Product;
using Nest.Services.CatalogApi.Services.Interfaces;
using Nest.Shared.DTO_s;

namespace Nest.Services.CatalogApi.Services.Implementations;

public class ProductService : IProductService
{
    public Task<ResponseDto<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<NoContent>> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<ProductDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<ProductDto>>> GetByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto)
    {
        throw new NotImplementedException();
    }
}
