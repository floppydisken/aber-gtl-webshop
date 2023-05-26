using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

/// <summary>
/// A value object to identify a product with values
/// </summary>
public class ProductDescription : ValueObject
{
    public required NonEmptyString SKU { get; set; }
    public required NonEmptyString Name { get; set; }
    public Total UnitPrice { get; set; } = Total.Zero;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SKU;
    }
}