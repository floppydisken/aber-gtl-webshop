using Webshop.Application.Contracts.Persistence;

namespace Webshop.Order.Persistence.Abstractions;

public interface IOrderRepository : IRepository<Domain.AggregateRoots.Order> {}