using MediatR;
using Nest.Services.Order.Application.DTO_s;
using Nest.Shared.DTO_s;

namespace Nest.Services.Order.Application.Queries;

public class GetOrdersByUserIdQuery : IRequest<ResponseDto<List<OrderDto>>>
{
    public string UserId { get; set; } = null!;
}
