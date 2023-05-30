using Vogen;
using Webshop.Domain.Common;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain;

public static class MappingExtensions
{
    private static Result<T> VogenToResult<T>(Func<T> map, Error error)
    {
        try
        {
            return Result.Ok<T>(map());
        }
        catch (ValueObjectValidationException)
        {
            return Result.Fail<T>(error);
        }
    }

    public static Result<Domain.AggregateRoots.Order> ToModel(this Domain.Dto.OrderDto dto)
    {
        var orderLinesResult = VogenToResult(
            () => NonEmptyList.From(dto.OrderLines.Select(ol => ol.ToModel().Unwrap())),
            Errors.General.ValueIsEmpty(nameof(dto.OrderLines)));

        if (orderLinesResult.Failure)
        {
            return Result.Fail<Domain.AggregateRoots.Order>(orderLinesResult.Error);
        }

        return Result.Ok<Domain.AggregateRoots.Order>(new()
        {
            OrderLines = orderLinesResult.Unwrap(),
            Discount = Discount.FromOrBoundary(dto.Discount),
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            CustomerId = dto.CustomerId
        });
    }

    public static Result<Domain.Dto.OrderDto> ToDto(this Domain.AggregateRoots.Order model)
    {

        return Result.Ok<Domain.Dto.OrderDto>(new()
        {
            Created = model.Created,
            LastModified = model.LastModified,
            CustomerId = model.CustomerId,
            Discount = model.Discount.Value,
            OrderLines = model.OrderLines.Select(ol => ol.ToDto().Unwrap())
        });
    }

    public static Result<Domain.Entities.OrderLine> ToModel(this Domain.Dto.OrderLineDto dto)
    {
        return Result.Ok<Domain.Entities.OrderLine>(new()
        {
            Product = dto.Product.ToModel(),
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            Quantity = Quantity.FromOrBoundary(dto.Quantity),
            Total = Total.FromOrBoundary(dto.Total)
        });
    }

    public static Result<Domain.Dto.OrderLineDto> ToDto(this Domain.Entities.OrderLine dto)
    {
        return Result.Ok<Domain.Dto.OrderLineDto>(new()
        {
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            Product = dto.Product.ToDto().Unwrap(),
            Quantity = dto.Quantity.Value,
            Total = dto.Total.Value
        });
    }

    public static Result<Domain.ValueObjects.ProductDescription> ToModel(this Domain.Dto.ProductDto dto)
    {
        var nameResult = VogenToResult(() => NonEmptyString.From(dto.Name), Errors.General.ValueIsEmpty(nameof(dto.Name)));
        if (nameResult.Failure)
        {
            return Result.Fail<Domain.ValueObjects.ProductDescription>(nameResult.Error);
        }

        var skuResult = VogenToResult(() => NonEmptyString.From(dto.SKU), Errors.General.ValueIsEmpty(nameof(dto.SKU)));
        if (skuResult.Failure)
        {
            return Result.Fail<Domain.ValueObjects.ProductDescription>(skuResult.Error);
        }

        return Result.Ok<Domain.ValueObjects.ProductDescription>(new()
        {
            Name = nameResult.Unwrap(),
            SKU = skuResult.Unwrap(),
            UnitPrice = Total.From(dto.UnitPrice)
        });
    }

    public static Result<Domain.Dto.ProductDto> ToDto(this Domain.ValueObjects.ProductDescription model)
    {
        return Result.Ok<Domain.Dto.ProductDto>(new()
        {
            Name = model.Name.Value,
            SKU = model.SKU.Value,
            UnitPrice = model.UnitPrice.Value
        });
    }

    public static Result<Domain.AggregateRoots.Voucher> ToModel(this Domain.Dto.VoucherDto dto)
    {
        return Result.Ok<Domain.AggregateRoots.Voucher>(new()
        {
            Id = dto.Id,
            Created = dto.Created,
            LastModified = dto.LastModified,
            Code = VoucherCode.From(dto.Code),
            Discount = Discount.FromOrBoundary(dto.Discount)
        });
    }

    public static Result<Domain.Dto.VoucherDto> ToDto(this Domain.AggregateRoots.Voucher model)
    {
        return Result.Ok<Domain.Dto.VoucherDto>(new()
        {
            Id = model.Id,
            Created = model.Created,
            LastModified = model.LastModified,

            Code = model.Code.Value,
            Discount = model.Discount.Value,
        });
    }
}