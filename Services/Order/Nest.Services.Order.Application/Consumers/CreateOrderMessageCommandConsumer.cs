using MassTransit;
using Nest.Services.Order.Infrastructure;
using Nest.Shared.Messages;

namespace Nest.Services.Order.Application.Consumers;

public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
{
    private readonly OrderDbContext _orderDbContext;

    public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        var newAddress = new Domain.OrderAggregate.Address(context.Message.Province, context.Message.District, context.Message.Street, context.Message.ZipCode, context.Message.Line);

        Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(newAddress, context.Message.BuyerId);

        context.Message.OrderItems.ForEach(x =>
        {
            order.AddOrderItem(x.ProductId, x.ProductName, (decimal)x.Price, x.PictureUrl);
        });

        await _orderDbContext.Orders.AddAsync(order);
        await _orderDbContext.SaveChangesAsync();
    }
}
