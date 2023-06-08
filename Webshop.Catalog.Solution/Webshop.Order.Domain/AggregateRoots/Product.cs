using Webshop.Domain.Common;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.AggregateRoots;

public class Product : AggregateRoot
{
    public required NonEmptyString SKU { get; set; }
    public required NonEmptyString Name { get; set; }
    public required Quantity AmountInStock { get; set; }
    public required Quantity MinStock { get; set; }
    public required Currency Currency { get; set; }
    public Total UnitPrice { get; set; } = Total.Zero;
}