using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class GetStoreWideDiscountQueryHandler : IGetStoreWideDiscountQueryHandler
{
    private readonly IVoucherRepository voucherRepository;

    public GetStoreWideDiscountQueryHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }
    
    public async Task<Result<Discount>> Handle(GetStoreWideDiscountQuery query, CancellationToken cancellationToken = default)
        => Result.Ok((await voucherRepository.GetByCodeAsync(VoucherCode.StoreWide.Value))?.Discount ?? Discount.Zero);
}