using MediatR;
using Microsoft.EntityFrameworkCore;
using Nest.Services.Order.Application.DTO_s;
using Nest.Services.Order.Application.Mapping;
using Nest.Services.Order.Application.Queries;
using Nest.Services.Order.Infrastructure;
using Nest.Shared.DTO_s;

namespace Nest.Services.Order.Application.Handlers;

public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQuery, ResponseDto<List<OrderDto>>>
{
    private readonly OrderDbContext _context;

    public GetOrdersByUserIdHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

        if (!orders.Any())
        {
            return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(), 200);
        }

        var orderDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

        return ResponseDto<List<OrderDto>>.Success(orderDto, 200);
    }
}
