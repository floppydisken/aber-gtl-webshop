using MediatR;
using Webshop.Domain.Common;
using Webshop.Order.Application.Abstractions;
using Webshop.Order.Persistence.Abstractions;

namespace Webshop.Order.Application.Features;

public class BuyCommandHandler : IBuyCommandHandler
{
    private readonly IOrderRepository orderRepository;
    private readonly IMediator mediator;

    public BuyCommandHandler(IOrderRepository orderRepository, IMediator mediator)
    {
        this.orderRepository = orderRepository;
        this.mediator = mediator;
    }

    public Task<Result> Handle(BuyCommand command, CancellationToken cancellationToken = default)
    {
        var products = 
        var total = 
        await this.orderRepository.CreateAsync(new() 
        {
            OrderLines = command.OrderLines.Select(ol => 
            {
                new() 
                {
                    ol.Quantity
                }
            })
        });
    }
}
