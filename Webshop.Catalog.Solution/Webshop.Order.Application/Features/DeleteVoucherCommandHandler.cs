using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
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
        try
        {
            await voucherRepository.DeleteByCodeAsync(command.Code);
            return Result.Ok();
        }
        catch(Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}