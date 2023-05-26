using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class BuyCommandHandler : IBuyCommandHandler
{
    private readonly IOrderRepository orderRepository;

    public BuyCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public Task<Result> Handle(BuyCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
