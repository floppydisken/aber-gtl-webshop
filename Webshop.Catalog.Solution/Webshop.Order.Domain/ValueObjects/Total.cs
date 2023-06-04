using System.Runtime.CompilerServices;
using Vogen;
using Webshop.Domain.Common;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain;

[ValueObject<decimal>]
[Instance("Zero", 0)]
[Instance("Min", 0)]
public partial struct Total 
{
    private static readonly decimal MinValue = 0;

    public static Validation Validate(decimal amount)
        => amount > Total.MinValue
            ? Validation.Ok
            : Validation.Invalid(Errors.General.ValueTooSmall(nameof(amount), Total.MinValue, "That'd be giving money away.").Message.Value);

    public static Total FromOrBoundary(decimal amount)
        => amount > Total.MinValue ? Total.From(amount) : Total.Min;

    public int ToMinorUnit()
        => Convert.ToInt32(this.Value * 100);
    
    public static Total operator +(Total lhs, Total rhs) => From(lhs.Value + rhs.Value);
    public static Total operator -(Total lhs, Total rhs) => From(lhs.Value - rhs.Value);
    public static Total operator +(decimal lhs, Total rhs) => From(lhs + rhs.Value);
    public static Total operator +(Total lhs, decimal rhs) => From(lhs.Value + rhs);
    public static Total operator *(Total lhs, decimal rhs) => From(lhs.Value * rhs);
    public static Total operator *(Total lhs, int rhs) => From(lhs.Value * rhs);
    public static Total operator *(Total lhs, Quantity quantity) => From(lhs.Value * quantity.Value);
    public static Total operator /(Total lhs, int rhs) => From(lhs.Value / rhs);
    public static Total operator /(Total lhs, decimal rhs) => From(lhs.Value / rhs);
    public static Total operator /(Total lhs, Total rhs) => From(lhs.Value / rhs.Value);
}