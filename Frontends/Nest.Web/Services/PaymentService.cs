﻿using Nest.Web.Models.FakePayment;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
    {
        var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("payments", paymentInfoInput);

        return response.IsSuccessStatusCode;
    }
}
