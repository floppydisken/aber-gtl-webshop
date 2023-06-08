using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class GetOrderQueryHandler : IGetOrderQueryHandler
{
    private readonly IOrderRepository orderRepository;

    public GetOrderQueryHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }
    
    public async Task<Result<Domain.AggregateRoots.Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId);

            return Result.Ok(order);
        }
        catch
        {
            return Result.Fail<Domain.AggregateRoots.Order>(Errors.General.NotFound(request.OrderId));
        }
    }
}