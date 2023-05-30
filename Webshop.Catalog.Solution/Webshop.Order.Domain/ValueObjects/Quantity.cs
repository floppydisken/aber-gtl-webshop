using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<int>]
[Instance("Zero", 0)]
[Instance("Min", 0)]
public partial struct Quantity
{
    private static readonly int MinValue = 0;

    public static Validation Validate(int value) =>
        value >= Quantity.MinValue 
            ? Validation.Ok 
            : Validation.Invalid(Errors.General.ValueTooSmall(nameof(value), Quantity.MinValue).Message.Value);

    public static Quantity FromOrBoundary(int value)
        => value < Quantity.MinValue ? Quantity.From(value) : Quantity.Min;
}