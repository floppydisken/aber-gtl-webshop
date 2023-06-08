using Webshop.Domain.Common;
using Webshop.Payment.Api.Models;

namespace Webshop.Payment.Client;

public interface IPaymentClient
{
    Task<Result<Transaction>> ProcessPayment(PaymentRequest request);
}