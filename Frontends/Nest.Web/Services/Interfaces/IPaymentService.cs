using Nest.Web.Models.FakePayment;

namespace Nest.Web.Services.Interfaces;

public interface IPaymentService
{
    Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
}
