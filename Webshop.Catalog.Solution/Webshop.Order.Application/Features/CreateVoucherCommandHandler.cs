using MediatR;
using Webshop.Application.Contracts;
using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class CreateVoucherCommandHandler : ICreateVoucherCommandHandler
{
    private readonly IVoucherRepository voucherRepository;

    public CreateVoucherCommandHandler(IVoucherRepository voucherRepository)
    {
        this.voucherRepository = voucherRepository;
    }
    
    public Task<Result> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}