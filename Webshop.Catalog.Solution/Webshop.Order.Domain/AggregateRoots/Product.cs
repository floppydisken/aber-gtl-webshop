using Webshop.Domain.Common;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.AggregateRoots;

public class Product : AggregateRoot
{
    public required NonEmptyString SKU { get; set; }
    public required NonEmptyString Name { get; set; }
    public Total UnitPrice { get; set; } = Total.Zero;
}