using Microsoft.AspNetCore.Mvc;
using Nest.Services.PaymentApi.DTO_s;
using Nest.Shared.ControllerBases;
using Nest.Shared.DTO_s;

namespace Nest.Services.PaymentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
    {
        return CreateActionResultInstance(ResponseDto<NoContent>.Success(200));
    }
}
