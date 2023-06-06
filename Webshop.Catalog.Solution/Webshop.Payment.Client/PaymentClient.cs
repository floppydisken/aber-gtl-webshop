using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using Webshop.Api.Utilities;
using Webshop.Payment.Api.Models;
using Webshop.Domain.Common;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Webshop.Payment.Client;

public class PaymentClientOptions
{
    public Uri PaymentUri { get; set; }
}

public class PaymentClient : IPaymentClient
{
    private readonly HttpClient client;
    private readonly PaymentClientOptions options;

    private static readonly JsonSerializerOptions jsonSettings = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public PaymentClient(HttpClient client, PaymentClientOptions options)
    {
        this.client = client;
        this.options = options;
    }

    public async Task<Result<Transaction>> ProcessPayment(PaymentRequest request)
    {
        var body = JsonContent.Create(request);
        var response = await client.PostAsync($"{options.PaymentUri}api/payment/process", body);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail<Transaction>($"Failed to create payment with message: {response}");
        }

        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonSerializer.Deserialize<Transaction>(content);

        if (deserialized is null)
        {
            return Result.Fail<Transaction>(Errors.General.ValueIsNull(nameof(deserialized)));
        }

        return deserialized;
    }
}