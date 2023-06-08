using Webshop.Application.Contracts;

namespace Webshop.Order.Application.Abstractions;

public class GetOrderQuery : IQuery<Domain.AggregateRoots.Order>
{
    public int OrderId { get; set; }
}