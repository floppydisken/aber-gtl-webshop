using System.Net.Http.Json;
using System.Text.Json;
using Webshop.Payment.Api.Models;
using Webshop.Domain.Common;

namespace Webshop.Payment.Client;

public class PaymentClientOptions
{
    public Uri PaymentUri { get; set; }
}

public class PaymentClient : IPaymentClient
{
    private readonly HttpClient client;
    private readonly PaymentClientOptions options;

    public PaymentClient(HttpClient client, PaymentClientOptions options)
    {
        this.client = client;
        this.options = options;
    }

    public async Task<Result<Transaction>> ProcessPayment(PaymentRequest request)
    {
        var body = JsonContent.Create(JsonSerializer.Serialize(request));
        var response = await this.client.PostAsync(options.PaymentUri, body);
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonSerializer.Deserialize<Transaction>(content);

        if (deserialized is null)
        {
            return Result.Fail<Transaction>(Errors.General.ValueIsNull(nameof(deserialized)));
        }

        return Result.Ok<Transaction>(deserialized);
    }
}