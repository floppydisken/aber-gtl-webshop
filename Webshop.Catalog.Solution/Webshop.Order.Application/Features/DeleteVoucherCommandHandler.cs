using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class DeleteVoucherCommandHandler : IDeleteVoucherCommandHandler
{
    private readonly IVoucherRepository voucherRepository;

    public DeleteVoucherCommandHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }
    
    public async Task<Result> Handle(DeleteVoucherCommand command, CancellationToken cancellationToken = default)
    {
        var voucherCodeResult = Result
            .Try(() => VoucherCode.From(command.Code))
            .Catch((e) => Errors.General.ValueIsInvalid(nameof(command.Code), e.Message))
            .Build();

        if (voucherCodeResult.Failure) 
            return voucherCodeResult;
        
        try
        {
            await voucherRepository.DeleteByCodeAsync(voucherCodeResult);
            return Result.Ok();
        }
        catch(Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}