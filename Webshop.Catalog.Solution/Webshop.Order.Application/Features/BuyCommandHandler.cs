using MediatR;
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
    private readonly CatalogClient catalogClient;
    private readonly IPaymentClient paymentClient;

    public BuyCommandHandler(
        IOrderRepository orderRepository, 
        IVoucherRepository voucherRepository,
        CatalogClient catalogClient, 
        IPaymentClient paymentClient
    ) {
        this.orderRepository = orderRepository;
        this.voucherRepository = voucherRepository;
        this.catalogClient = catalogClient;
        this.paymentClient = paymentClient;
    }

    public async Task<Result> Handle(BuyCommand command, CancellationToken cancellationToken = default)
    {
        var products = await catalogClient.GetAllAsync(command.OrderLines.Select(ol => ol.ProductId));
        const string storeWideDiscountCode = "STORE_WIDE";

        Voucher? voucher = null;
        if (command.VoucherCode is not null && command.VoucherCode != storeWideDiscountCode)
        {
            voucher = await voucherRepository.GetByCodeAsync(command.VoucherCode);
        }

        var storeWideVoucher = await voucherRepository.GetByCodeAsync(storeWideDiscountCode);

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
            Discount = (voucher?.Discount ?? Discount.Zero) + (storeWideVoucher?.Discount ?? Discount.Zero),
            CustomerId = command.CustomerId,
        });

        return Result.Ok();
    }
}
