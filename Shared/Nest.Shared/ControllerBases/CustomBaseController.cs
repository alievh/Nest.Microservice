using Microsoft.AspNetCore.Mvc;
using Nest.Shared.DTO_s;

namespace Nest.Shared.ControllerBases;

public class CustomBaseController : ControllerBase
{
    public static IActionResult CreateActionResultInstance<T>(ResponseDto<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}
