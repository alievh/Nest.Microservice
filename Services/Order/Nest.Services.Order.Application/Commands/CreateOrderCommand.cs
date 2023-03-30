using MediatR;
using Nest.Services.Order.Application.DTO_s;
using Nest.Shared.DTO_s;

namespace Nest.Services.Order.Application.Commands;

public class CreateOrderCommand : IRequest<ResponseDto<CreatedOrderDto>>
{
    public string BuyerId { get; set; } = null!;
    public List<OrderItemDto>? OrderItems { get; set; }

    public AddressDto? Address { get; set; }
}
