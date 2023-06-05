using System.ComponentModel.DataAnnotations;
using Vogen;
using Webshop.Domain.Common;
using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<string>]
[Instance("StoreWide", "STORE_WIDE")]
public partial struct VoucherCode
{
    public static Validation Validate(string value) =>
        value == StoreWide
            ? Validation.Invalid(Errors.General
                .ValueIsInvalid(nameof(value), $"'{value}' is reserved for the store wide discount.").Message.Value)
            : string.IsNullOrWhiteSpace(value)
            ? Validation.Invalid(Errors.General.ValueIsEmpty(nameof(value)).Message.Value)
            : Validation.Ok;
}