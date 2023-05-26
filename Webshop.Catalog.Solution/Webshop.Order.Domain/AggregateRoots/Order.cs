using Webshop.Domain.Common;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.AggregateRoots;

public class Order : AggregateRoot
{ 
    public OrderStatus Status { get; set; } = OrderStatus.Created;
    public Discount Discount { get; set; } = Discount.Zero;
    public required NonEmptyList<OrderLine> OrderLines { get; set; }
}