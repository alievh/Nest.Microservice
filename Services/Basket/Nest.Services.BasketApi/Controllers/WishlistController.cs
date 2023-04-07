using Microsoft.AspNetCore.Mvc;
using Nest.Services.BasketApi.DTO_s;
using Nest.Services.BasketApi.Services;
using Nest.Shared.ControllerBases;
using Nest.Shared.Services;

namespace Nest.Services.BasketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : CustomBaseController
    {
        private readonly IWishlistService _wishlistService;
        private readonly ISharedIdentityService _identityService;

        public WishlistController(IWishlistService wishlistService, ISharedIdentityService identityService)
        {
            _wishlistService = wishlistService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
            => CreateActionResultInstance(await _wishlistService.GetWishlist(_identityService.GetUserId));

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(WishlistDto wishlistDto)
        {
            wishlistDto.UserId = _identityService.GetUserId;
            var response = await _wishlistService.SaveOrUpdate(wishlistDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
            => CreateActionResultInstance(await _wishlistService.DeleteWishlist(_identityService.GetUserId));
    }
}
