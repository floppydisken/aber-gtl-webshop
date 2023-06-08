using Webshop.Application.Contracts;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Abstractions;

public interface IGetStoreWideDiscountQueryHandler : IQueryHandler<GetStoreWideDiscountQuery, Discount>
{
}