using Webshop.Domain.Common;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.AggregateRoots;

public class Order : AggregateRoot
{ 
    public Total Total => OrderLines.Aggregate(
        Total.Zero, 
        (current, orderLine) => current + orderLine.Total - (orderLine.Total * Discount.Pct)
    );
    public OrderStatus Status { get; set; } = OrderStatus.Created;
    public Discount Discount { get; set; } = Discount.Zero;
    public required NonEmptyEntityList<OrderLine> OrderLines { get; set; }
    public int CustomerId { get; set; }
    public Guid? TransactionId { get; set; }
}