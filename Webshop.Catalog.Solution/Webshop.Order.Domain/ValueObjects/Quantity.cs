using System.Numerics;
using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<int>]
[Instance("Zero", 0)]
[Instance("Min", 0)]
public partial struct Quantity : IComparisonOperators<Quantity, int, bool>
{
    private static readonly int MinValue = 0;

    public static Validation Validate(int value) =>
        value >= Quantity.MinValue 
            ? Validation.Ok 
            : Validation.Invalid(Errors.General.ValueTooSmall(nameof(value), Quantity.MinValue).Message.Value);

    public static Quantity FromOrBoundary(int value)
        => value < Quantity.MinValue ? Quantity.From(value) : Quantity.Min;
        
    public static Quantity operator +(Quantity lhs, Quantity rhs) => FromOrBoundary(lhs.Value + rhs.Value);
    public static Quantity operator -(Quantity lhs, Quantity rhs) => FromOrBoundary(lhs.Value - rhs.Value);
    public static Quantity operator -(Quantity lhs, int rhs) => FromOrBoundary(lhs.Value - rhs);
    public static bool operator >(Quantity left, int right) => left.Value > right;
    public static bool operator >=(Quantity left, int right) => left.Value >= right;
    public static bool operator <(Quantity left, int right) => left.Value < right;
    public static bool operator <=(Quantity left, int right) => left.Value <= right;
}