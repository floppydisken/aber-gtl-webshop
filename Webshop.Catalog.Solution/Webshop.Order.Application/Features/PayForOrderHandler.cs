using Microsoft.Extensions.Logging;
using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;
using Webshop.Payment.Client;

namespace Webshop.Order.Application;

public class PayForOrderCommandHandler : IPayForOrderCommandHandler
{
    private readonly IPaymentClient paymentClient;
    private readonly IOrderRepository orderRepository;
    private readonly ILogger<PayForOrderCommandHandler> logger;

    public PayForOrderCommandHandler(IPaymentClient paymentClient, IOrderRepository orderRepository, ILogger<PayForOrderCommandHandler> logger)
    {
        this.paymentClient = paymentClient;
        this.orderRepository = orderRepository;
        this.logger = logger;
    }

    public async Task<Result> Handle(PayForOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(command.OrderId);

        if (order.Status != OrderStatus.Created)
        {
            this.logger.LogError("Could not move order status to '{OrderStatus}' because the Order with ID '{OrderID}' is in state '{CurrentOrderStatus}' and not '{OrderStatus}'",
                OrderStatus.Pending, order.Id, order.Status);
            return Result.Fail(new Error("order.wrong.state", $"Order with ID '{command.OrderId}' is in the wrong state. It should be in the '{OrderStatus.Created}' state"));
        }

        var transaction = await paymentClient.ProcessPayment(new(order.Total.ToMinorUnit(), command.CardNumber, command.ExpirationDate, command.CVC));

        if (transaction.Failure)
        {
            this.logger.LogError("Could not create payment for Order with ID {OrderID}", order.Id);
            return Result.Fail(new Error(
                "order.payment",
                $"Failed to process payment for Order with ID {order.Id}. "
              + $"Failed with message: '{transaction.Error.Message.Value}'"
            ));
        }

        try
        {
            order.Status = OrderStatus.Pending;
            order.TransactionId = transaction.Unwrap().TransactionId;

            await orderRepository.UpdateAsync(order);
            logger.LogInformation("Moved Order with ID '{OrderID}' to status '{OrderStatus}'", order.Id, OrderStatus.Pending);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }

        return Result.Ok();
    }
}