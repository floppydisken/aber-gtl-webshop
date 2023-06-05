using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;

namespace Webshop.Order.Application.Features;

public class DeleteVoucherCommandHandler : IDeleteVoucherCommandHandler
{
    public Task<Result> Handle(DeleteVoucherCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}