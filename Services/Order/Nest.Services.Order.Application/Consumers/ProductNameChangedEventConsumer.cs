using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nest.Services.Order.Infrastructure;
using Nest.Shared.Messages;

namespace Nest.Services.Order.Application.Consumers;


public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
{
    private readonly OrderDbContext _orderDbContext;

    public ProductNameChangedEventConsumer(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
    {
        var orderItems = await _orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToListAsync();

        orderItems.ForEach(x =>
        {
            x.UpdateOrderItem(context.Message.UpdatedName, x.PictureUrl, x.Price);
        });

        await _orderDbContext.SaveChangesAsync();
    }

}
