using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<decimal>]
[Instance("Zero", 0)]
[Instance("Min", 0)]
[Instance("Max", 15)]
public partial struct Discount
{
    private const decimal MinValue = 0;
    private const decimal MaxValue = 15;

    public static Validation Validate(decimal value) =>
        value < MinValue
            ? Validation.Invalid(Errors.General.ValueTooSmall(nameof(value), MinValue).Message.Value)
            : value > MaxValue
            ? Validation.Invalid(Errors.General.ValueTooLarge(nameof(value), MaxValue).Message.Value)
            : Validation.Ok;

    public static Discount FromOrBoundary(decimal value)
        => value > MaxValue ? Discount.Max : value < MinValue ? Discount.Min : From(value);

    public decimal Pct => Value / 100;
    
    public static Discount operator +(Discount lhs, Discount rhs) => FromOrBoundary(lhs.Value + rhs.Value);
    public static Discount operator -(Discount lhs, Discount rhs) => FromOrBoundary(lhs.Value - rhs.Value);
}