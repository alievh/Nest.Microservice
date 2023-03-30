using Microsoft.AspNetCore.Mvc;
using Nest.Services.DiscountApi.Services;
using Nest.Shared.ControllerBases;
using Nest.Shared.Services;

namespace Nest.Services.DiscountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IDiscountService _discountService;

        public DiscountsController(ISharedIdentityService identityService, IDiscountService discountService)
        {
            _identityService = identityService;
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => CreateActionResultInstance(await _discountService.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetById(id);

            return CreateActionResultInstance(discount);
        }

        [HttpGet]
        [Route("[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _identityService.GetUserId;
            var discount = await _discountService.GetByCodeAndUserId(code, userId);

            return CreateActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
            => CreateActionResultInstance(await _discountService.Save(discount));

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
            => CreateActionResultInstance(await _discountService.Update(discount));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => CreateActionResultInstance(await _discountService.Delete(id));
    }
}
