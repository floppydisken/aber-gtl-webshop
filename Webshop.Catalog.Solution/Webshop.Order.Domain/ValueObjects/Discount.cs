using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<decimal>]
[Instance("Zero", 0)]
[Instance("Min", 0)]
[Instance("Max", 15)]
public partial struct Discount
{
    private static readonly decimal MinValue = 0;
    private static readonly decimal MaxValue = 15;

    public static Validation Validate(decimal value) =>
        value < Discount.MinValue
            ? Validation.Invalid(Errors.General.ValueTooSmall(nameof(value), Discount.MinValue).Message.Value)
            : value > Discount.MaxValue
            ? Validation.Invalid(Errors.General.ValueTooLarge(nameof(value), Discount.MaxValue).Message.Value)
            : Validation.Ok;

    public static Discount FromOrBoundary(decimal value)
    {
        return value > MaxValue ? Discount.Max : value < MinValue ? Discount.Min : Discount.From(value);
    }
}