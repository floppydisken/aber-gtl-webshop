using MediatR;
using Webshop.Application.Contracts;
using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class CreateVoucherCommandHandler : ICreateVoucherCommandHandler
{
    private readonly IVoucherRepository voucherRepository;

    public CreateVoucherCommandHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }

    public async Task<Result> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
    {
        var discountResult = FluentVogen
            .UseMapper(() => Discount.From(request.Amount))
            .UseError((e) =>
                Errors.General.ValueOutOfRange(nameof(request.Amount), Discount.Min.Value, Discount.Max.Value,
                    e.Message))
            .Run();

        if (discountResult.Failure)
        {
            return discountResult;
        }

        try
        {
            await voucherRepository.CreateAsync(new()
            {
                Code = VoucherCode.From(request.Code),
                Discount = discountResult.UnwrapOr(Discount.FromOrBoundary(request.Amount))
            });

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}