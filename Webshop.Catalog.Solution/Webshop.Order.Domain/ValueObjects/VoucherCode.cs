using Vogen;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

[ValueObject<string>]
[Instance("StoreWide", "STORE_WIDE")]
public partial struct VoucherCode
{
    private static Validation Validate(string value) =>
        value == StoreWide
            ? Validation.Invalid(Errors.General
                .ValueIsInvalid(nameof(value), $"'{value}' is reserved for the store wide discount.").Message.Value)
            : string.IsNullOrWhiteSpace(value)
            ? Validation.Invalid(Errors.General.ValueIsEmpty(nameof(value)).Message.Value)
            : Validation.Ok;
}