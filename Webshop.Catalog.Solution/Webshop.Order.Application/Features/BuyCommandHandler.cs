using Webshop.Application.Contracts;
using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;
using Webshop.Payment.Client;

namespace Webshop.Order.Application.Features;

public class BuyCommandHandler : IBuyCommandHandler
{
    private readonly IOrderRepository orderRepository;
    private readonly IVoucherRepository voucherRepository;
    private readonly IDispatcher dispatcher;
    private readonly CatalogClient catalogClient;
    private readonly IPaymentClient paymentClient;

    public BuyCommandHandler(
        IOrderRepository orderRepository,
        IVoucherRepository voucherRepository,
        IDispatcher dispatcher,
        CatalogClient catalogClient,
        IPaymentClient paymentClient
    )
    {
        this.orderRepository = orderRepository;
        this.voucherRepository = voucherRepository;
        this.dispatcher = dispatcher;
        this.catalogClient = catalogClient;
        this.paymentClient = paymentClient;
    }

    public async Task<Result> Handle(BuyCommand command, CancellationToken cancellationToken = default)
    {
        var products = (await catalogClient
                .GetAllAsync(command.OrderLines.Select(ol => ol.ProductId)))
            .UnwrapOr(Array.Empty<Product>())
            .ToArray();

        var nonExistingProducts = command.OrderLines
            .Where(ol => products.All(p => p.Id != ol.ProductId))
            .ToArray();

        if (nonExistingProducts.Any())
        {
            return Result.Fail(Errors.General.ValueIsInvalid(
                $"IDs [{string.Join(", ", nonExistingProducts.Select(p => p.ProductId))}]",
                "Could not find products"));
        }

        var productsNotInStock = products
            .Where(p => p.AmountInStock == Quantity.Zero)
            .ToArray();

        var stocks = products.Select(p => p.AmountInStock.Value);

        if (productsNotInStock.Any())
        {
            return Result.Fail(Errors.General.ValueIsInvalid(
                $"IDs [{string.Join(", ", productsNotInStock.Select(p => p.Id))}]",
                "The products are not in stock."));
        }

        var productsWhereQuantitiesExceedStock = products
            .Where(p => (p.AmountInStock - command.OrderLines.First(ol => ol.ProductId == p.Id).Quantity) < p.MinStock)
            .ToArray();

        if (productsWhereQuantitiesExceedStock.Any())
        {
            return Result.Fail(Errors.General.ValueIsInvalid(
                $"IDs [{string.Join(", ", productsNotInStock.Select(p => p.Id))}]",
                "Products do not have the stock required to fulfill the order."
            ));
        }

        var orderLines = NonEmptyEntityList.From(command.OrderLines.Select(ol =>
        {
            var product = products.First(p => p.Id == ol.ProductId);

            product.AmountInStock -= ol.Quantity; // Side effect, can this be mitigated?

            var orderLine = new OrderLine()
            {
                Product = product.ToDescription(),
                Quantity = Quantity.From(ol.Quantity),
            };

            return orderLine;
        }).ToArray());

        if (orderLines.Failure)
        {
            return Result.Fail(orderLines.Error);
        }

        var voucherCodeResult = FluentVogen
            .UseMapper(() => VoucherCode.From(command.VoucherCode))
            .UseError(e => Errors.General.ValueIsInvalid(nameof(command.VoucherCode), e.Message))
            .Run();

        var voucher = voucherCodeResult.Success
            ? await voucherRepository.GetByCodeAsync(voucherCodeResult)
            : default;

        var storeWideDiscount = (await dispatcher.Dispatch(new GetStoreWideDiscountQuery()))
            .UnwrapOr(Discount.Zero);

        var order = new Domain.AggregateRoots.Order()
        {
            Id = command.OrderId,
            OrderLines = orderLines.Unwrap(),
            Discount = (voucher?.Discount ?? Discount.Zero) + storeWideDiscount,
            CustomerId = command.CustomerId,
        };

        await orderRepository.CreateAsync(order);

        foreach (var product in products)
        {
            await catalogClient.UpdateAsync(product); // We want the stock updated
        }

        return Result.Ok();
    }
}