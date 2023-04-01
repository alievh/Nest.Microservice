using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Nest.Services.PaymentApi.DTO_s;
using Nest.Shared.ControllerBases;
using Nest.Shared.DTO_s;
using Nest.Shared.Messages;

namespace Nest.Services.PaymentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : CustomBaseController
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public PaymentsController(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost]
    public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
    {
        ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

        var createOrderMessageCommand = new CreateOrderMessageCommand();
        createOrderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
        createOrderMessageCommand.Province = paymentDto.Order.Address.Province;
        createOrderMessageCommand.District = paymentDto.Order.Address.District;
        createOrderMessageCommand.Street = paymentDto.Order.Address.Street;
        createOrderMessageCommand.Line = paymentDto.Order.Address.Line;
        createOrderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;

        paymentDto.Order.OrderItems.ForEach(x =>
        {
            createOrderMessageCommand.OrderItems.Add(new OrderItem
            {
                PictureUrl = x.PictureUrl,
                Price = x.Price,
                ProductId = x.ProductId,
                ProductName = x.ProductName
            });
        });

        await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

        return CreateActionResultInstance(ResponseDto<NoContent>.Success(200));
    }
}
