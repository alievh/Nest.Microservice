using Nest.Services.BasketApi.DTO_s;
using Nest.Shared.DTO_s;
using System.Text.Json;

namespace Nest.Services.BasketApi.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
    {
        var existBasket = await _redisService.GetDb().StringGetAsync(userId);

        if (String.IsNullOrEmpty(existBasket))
        {
            return ResponseDto<BasketDto>.Fail("Basket not found!", 404);
        }

        return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
    }

    public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

        return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket couldn't update or save", 500);
    }

    public async Task<ResponseDto<bool>> DeleteBasket(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket not found", 404);
    }
}
