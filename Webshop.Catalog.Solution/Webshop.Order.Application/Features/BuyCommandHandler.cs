using MediatR;
using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;
using Webshop.Payment.Client;

namespace Webshop.Order.Application.Features;

public class BuyCommandHandler : IBuyCommandHandler
{
    private readonly IOrderRepository orderRepository;
    private readonly CatalogClient catalogClient;
    private readonly IPaymentClient paymentClient;

    public BuyCommandHandler(
        IOrderRepository orderRepository, 
        CatalogClient catalogClient, 
        IPaymentClient paymentClient
    ) {
        this.orderRepository = orderRepository;
        this.catalogClient = catalogClient;
        this.paymentClient = paymentClient;
    }

    public async Task<Result> Handle(BuyCommand command, CancellationToken cancellationToken = default)
    {
        var products = await catalogClient.GetAllAsync(command.OrderLines.Select(ol => ol.ProductId));

        var total = 0m;
        foreach(var product in products.Unwrap())
        {
            total += product.UnitPrice.Value * command.OrderLines.First(ol => ol.ProductId == product.Id).Quantity;
        }

        await this.orderRepository.CreateAsync(new() 
        {
            Id = command.OrderId,
            OrderLines = NonEmptyEntityList.From(command.OrderLines.Select(ol => 
            {
                return new OrderLine() 
                {
                    Total = Total.FromOrBoundary(total),
                    Product = products.Unwrap().First(p => p.Id == ol.ProductId).ToDescription(),
                    Quantity = Quantity.From(ol.Quantity),
                };
            })),
            Discount = Discount.Zero,
            CustomerId = command.CustomerId,
        });

        return Result.Ok();
    }
}
