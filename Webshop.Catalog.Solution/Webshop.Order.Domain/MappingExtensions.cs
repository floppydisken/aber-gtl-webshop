using Vogen;
using Webshop.Domain.Common;
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

    public static Result<AggregateRoots.Order> ToModel(this Dto.OrderDto dto)
    {
        var orderLinesResult = VogenToResult(
            () => NonEmptyEntityList.From(dto.OrderLines.Select(ol => ol.ToModel().Unwrap())),
            Errors.General.ValueIsEmpty(nameof(dto.OrderLines)));

        if (orderLinesResult.Failure)
        {
            return Result.Fail<AggregateRoots.Order>(orderLinesResult.Error);
        }

        return Result.Ok<AggregateRoots.Order>(new()
        {
            OrderLines = orderLinesResult.Unwrap(),
            Discount = Discount.FromOrBoundary(dto.Discount),
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            CustomerId = dto.CustomerId
        });
    }

    public static Result<Dto.OrderDto> ToDto(this AggregateRoots.Order model)
    {

        return Result.Ok<Dto.OrderDto>(new()
        {
            Created = model.Created,
            LastModified = model.LastModified,
            CustomerId = model.CustomerId,
            Discount = model.Discount.Value,
            OrderLines = model.OrderLines.Select(ol => ol.ToDto().Unwrap())
        });
    }

    public static Result<Entities.OrderLine> ToModel(this Dto.OrderLineDto dto)
    {
        return Result.Ok<Entities.OrderLine>(new()
        {
            Product = dto.Product
                .ToModel()
                .Unwrap()
                .ToDescription()
                .Unwrap(),
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            Quantity = Quantity.FromOrBoundary(dto.Quantity),
        });
    }

    public static Result<Dto.OrderLineDto> ToDto(this Entities.OrderLine dto)
    {
        return Result.Ok<Dto.OrderLineDto>(new()
        {
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            Product = dto.Product.ToDto().Unwrap(),
            Quantity = dto.Quantity.Value,
            Total = dto.Total.Value
        });
    }

    public static Result<AggregateRoots.Product> ToModel(this Dto.ProductDto dto)
    {
        var nameResult = FluentVogen
            .UseMapper(() => NonEmptyString.From(dto.Name))
            .UseError((e) => Errors.General.ValueIsEmpty(nameof(dto.Name)))
            .Run();
 
        if (nameResult.Failure)
        {
            return Result.Fail<AggregateRoots.Product>(nameResult.Error);
        }

        var skuResult = FluentVogen
            .UseMapper(() => NonEmptyString.From(dto.SKU))
            .UseError(_ => Errors.General.ValueIsEmpty(nameof(dto.SKU)))
            .Run();
        if (skuResult.Failure)
        {
            return Result.Fail<AggregateRoots.Product>(skuResult.Error);
        }

        var amountInStock = FluentVogen
            .UseMapper(() => Quantity.FromOrBoundary(dto.AmountInStock))
            .Run();

        var minStock = FluentVogen
            .UseMapper(() => Quantity.FromOrBoundary(dto.MinStock))
            .Run();

        if (!Enum.TryParse(dto.Currency, out Currency currency))
        {
            return Result.Fail<AggregateRoots.Product>(
                Errors.General.ValueIsInvalid(nameof(dto.Currency), 
                    $"Could not parse {dto.Currency}. Values has to be one of [{string.Join(", ", Enum.GetValues<Currency>().Select(e => e.ToString()))}]."
                ));
        }

        return Result.Ok<AggregateRoots.Product>(new()
        {
            Id = dto.Id,
            Created = dto.Created,
            LastModified = dto.LastModified,

            Name = nameResult.Unwrap(),
            SKU = skuResult.Unwrap(),
            UnitPrice = Total.From(dto.Price),
            
            AmountInStock = amountInStock,
            MinStock = minStock,
            
            Currency = currency
        });
    }

    public static Result<ValueObjects.ProductDescription> ToDescription(this AggregateRoots.Product model)
    {
        return Result.Ok<ValueObjects.ProductDescription>(new()
        {
            Name = model.Name,
            SKU = model.SKU,
            UnitPrice = model.UnitPrice
        });
    }

    public static Result<Dto.ProductDto> ToDto(this ValueObjects.ProductDescription model)
    {
        return Result.Ok<Dto.ProductDto>(new()
        {
            Name = model.Name.Value,
            SKU = model.SKU.Value,
            Price = model.UnitPrice.Value
        });
    }

    public static Result<AggregateRoots.Voucher> ToModel(this Dto.VoucherDto dto)
    {
        return Result.Ok<AggregateRoots.Voucher>(new()
        {
            Id = dto.Id,
            Created = dto.Created,
            LastModified = dto.LastModified,
            Code = VoucherCode.From(dto.Code),
            Discount = Discount.FromOrBoundary(dto.Discount)
        });
    }

    public static Result<Dto.VoucherDto> ToDto(this AggregateRoots.Voucher model)
    {
        return Result.Ok<Dto.VoucherDto>(new()
        {
            Id = model.Id,
            Created = model.Created,
            LastModified = model.LastModified,

            Code = model.Code.Value,
            Discount = model.Discount.Value,
        });
    }
}