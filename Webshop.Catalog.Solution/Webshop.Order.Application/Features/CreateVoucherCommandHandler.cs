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
        var voucherCodeResult = Result
            .Try(() => VoucherCode.From(request.Code))
            .Catch(e => Errors.General.ValueIsInvalid(
                nameof(request.Code),
                e.Message
            ))
            .Build();

        if (voucherCodeResult.Failure)
        {
            return voucherCodeResult;
        }

        var discountResult = Result
            .Try(() => Discount.From(request.Amount))
            .Catch((e) =>
                Errors.General.ValueOutOfRange(nameof(request.Amount), Discount.Min.Value, Discount.Max.Value,
                    e.Message))
            .Build();

        if (discountResult.Failure)
        {
            return discountResult;
        }

        try
        {
            await voucherRepository.CreateAsync(new()
            {
                Id = new Random().Next(),
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