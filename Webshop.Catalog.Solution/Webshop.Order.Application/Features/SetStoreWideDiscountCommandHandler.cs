using Vogen;
using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class SetStoreWideDiscountCommandHandler : ISetStoreWideDiscountCommandHandler
{
    private readonly IVoucherRepository voucherRepository;

    public SetStoreWideDiscountCommandHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }

    public async Task<Result> Handle(SetStoreWideDiscountCommand command, CancellationToken cancellationToken = default)
    {
        var discount = await voucherRepository.GetByCodeAsync(VoucherCode.StoreWide);

        var discountResult = FluentVogen
            .UseMapper(() => Discount.From(command.Amount))
            .UseError((e) =>
                Errors.General.ValueOutOfRange(
                    nameof(command.Amount),
                    Discount.Min.Value,
                    Discount.Max.Value,
                    e.Message
                )
            ).Run();

        if (discountResult.Failure)
        {
            return discountResult;
        }

        if (discount is null)
        {
            await voucherRepository.CreateAsync(new Voucher()
            {
                Discount = discountResult.UnwrapOr(Discount.Zero),
                Code = VoucherCode.StoreWide,
            });

            return Result.Ok();
        }

        discount.Discount = discountResult.UnwrapOr(Discount.Zero);
        await voucherRepository.UpdateAsync(discount);

        return Result.Ok();
    }
}