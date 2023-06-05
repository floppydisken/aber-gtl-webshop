using System.IO.Pipelines;
using MediatR;
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
    ) {
        this.orderRepository = orderRepository;
        this.voucherRepository = voucherRepository;
        this.dispatcher = dispatcher;
        this.catalogClient = catalogClient;
        this.paymentClient = paymentClient;
    }

    public async Task<Result> Handle(BuyCommand command, CancellationToken cancellationToken = default)
    {
        var products = await catalogClient.GetAllAsync(command.OrderLines.Select(ol => ol.ProductId));

        var voucherCodeResult = FluentVogen
            .UseMapper(() => VoucherCode.From(command.VoucherCode))
            .UseError(e => Errors.General.ValueIsInvalid(nameof(command.VoucherCode), e.Message))
            .Run();

        var voucher = voucherCodeResult.Success 
            ? await voucherRepository.GetByCodeAsync(voucherCodeResult) 
            : default;

        var storeWideDiscount = (await dispatcher.Dispatch(new GetStoreWideDiscountQuery()))
            .UnwrapOr(Discount.Zero);

        await orderRepository.CreateAsync(new() 
        {
            Id = command.OrderId,
            OrderLines = NonEmptyEntityList.From(command.OrderLines.Select(ol => 
            {
                return new OrderLine() 
                {
                    Product = products
                        .Unwrap()
                        .First(p => p.Id == ol.ProductId)
                        .ToDescription(),
                    Quantity = Quantity.From(ol.Quantity),
                };
            })),
            Discount = (voucher?.Discount ?? Discount.Zero) + storeWideDiscount,
            CustomerId = command.CustomerId,
        });

        return Result.Ok();
    }
}
