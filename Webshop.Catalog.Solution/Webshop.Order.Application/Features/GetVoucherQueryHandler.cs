using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class GetVoucherQueryHandler : IGetVoucherQueryHandler
{
    private readonly IVoucherRepository voucherRepository;

    public GetVoucherQueryHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }
    
    public async Task<Result<Discount>> Handle(GetVoucherQuery query, CancellationToken cancellationToken = default)
        => Result.Ok((await voucherRepository.GetByCodeAsync(VoucherCode.From(query.Code)))?.Discount ?? Discount.Zero);
}