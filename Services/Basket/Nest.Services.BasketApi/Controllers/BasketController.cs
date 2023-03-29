using Microsoft.AspNetCore.Mvc;
using Nest.Services.BasketApi.DTO_s;
using Nest.Services.BasketApi.Services;
using Nest.Shared.ControllerBases;
using Nest.Shared.Services;

namespace Nest.Services.BasketApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : CustomBaseController
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _identityService;

    public BasketController(IBasketService basketService, ISharedIdentityService identityService)
    {
        _basketService = basketService;
        _identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBasket()
        => CreateActionResultInstance(await _basketService.GetBasket(_identityService.GetUserId));

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
    {
        basketDto.UserId = _identityService.GetUserId;
        var response = await _basketService.SaveOrUpdate(basketDto);

        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
        => CreateActionResultInstance(await _basketService.DeleteBasket(_identityService.GetUserId));

}
