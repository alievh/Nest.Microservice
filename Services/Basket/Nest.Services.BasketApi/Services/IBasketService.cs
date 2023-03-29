using Nest.Services.BasketApi.DTO_s;
using Nest.Shared.DTO_s;

namespace Nest.Services.BasketApi.Services;

public interface IBasketService
{
    Task<ResponseDto<BasketDto>> GetBasket(string userId);
    Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);
    Task<ResponseDto<bool>> DeleteBasket(string userId);
}
