using Nest.Services.BasketApi.DTO_s;
using Nest.Shared.DTO_s;

namespace Nest.Services.BasketApi.Services;

public interface IWishlistService
{
    Task<ResponseDto<WishlistDto>> GetWishlist(string userId);
    Task<ResponseDto<bool>> SaveOrUpdate(WishlistDto wishlistDto);
    Task<ResponseDto<bool>> DeleteWishlist(string userId);
}
