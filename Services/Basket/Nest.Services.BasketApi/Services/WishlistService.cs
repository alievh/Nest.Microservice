using Nest.Services.BasketApi.DTO_s;
using Nest.Shared.DTO_s;
using System.Text.Json;

namespace Nest.Services.BasketApi.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly RedisService _redisService;

        public WishlistService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDto<WishlistDto>> GetWishlist(string userId)
        {
            var existWishlist = await _redisService.GetWishlistDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existWishlist))
            {
                return ResponseDto<WishlistDto>.Fail("Wishlist not found!", 404);
            }

            return ResponseDto<WishlistDto>.Success(JsonSerializer.Deserialize<WishlistDto>(existWishlist), 200);
        }

        public async Task<ResponseDto<bool>> DeleteWishlist(string userId)
        {
            var status = await _redisService.GetWishlistDb().KeyDeleteAsync(userId);
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Wishlist not found", 404);
        }

        public async Task<ResponseDto<bool>> SaveOrUpdate(WishlistDto wishlistDto)
        {
            var status = await _redisService.GetWishlistDb().StringSetAsync(wishlistDto.UserId, JsonSerializer.Serialize(wishlistDto));

            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Wishlist couldn't update or save", 500);
        }
    }
}
