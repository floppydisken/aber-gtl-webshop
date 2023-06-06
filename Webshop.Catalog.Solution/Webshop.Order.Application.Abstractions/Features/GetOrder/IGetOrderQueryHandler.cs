using Webshop.Application.Contracts;

namespace Webshop.Order.Application.Abstractions;

public interface IGetOrderQueryHandler : IQueryHandler<GetOrderQuery, Domain.AggregateRoots.Order>
{
}