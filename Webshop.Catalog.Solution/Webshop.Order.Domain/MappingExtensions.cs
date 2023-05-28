using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain;

public static class MappingExtensions
{
    public static Domain.AggregateRoots.Order ToModel(this Domain.Dto.OrderDto dto)
    {
        return new()
        {
            OrderLines = NonEmptyList.From(dto.OrderLines.Select(ol => ol.ToModel())),
            Discount = Discount.From(dto.Discount),
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            CustomerId = dto.CustomerId
        };
    }

    public static Domain.Dto.OrderDto ToDto(this Domain.AggregateRoots.Order model)
    {

        return new()
        {
            Created = model.Created,
            LastModified = model.LastModified,
            CustomerId = model.CustomerId,
            Discount = model.Discount.Value,
            OrderLines = model.OrderLines.Select(ol => ol.ToDto())
        };
    }

    public static Domain.Entities.OrderLine ToModel(this Domain.Dto.OrderLineDto dto)
    {
        return new()
        {
            Product = dto.Product.ToModel(),
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            Quantity = Quantity.From(dto.Quantity),
            Total = Total.From(dto.Total)
        };
    }

    public static Domain.Dto.OrderLineDto ToDto(this Domain.Entities.OrderLine dto)
    {
        return new()
        {
            Created = dto.Created,
            LastModified = dto.LastModified,
            Id = dto.Id,
            Product = dto.Product.ToDto(),
            Quantity = dto.Quantity.Value,
            Total = dto.Total.Value
        };
    }

    public static Domain.ValueObjects.ProductDescription ToModel(this Domain.Dto.ProductDto dto)
    {
        return new()
        {
            Name = NonEmptyString.From(dto.Name),
            SKU = NonEmptyString.From(dto.SKU),
            UnitPrice = Total.From(dto.UnitPrice)
        };
    }

    public static Domain.Dto.ProductDto ToDto(this Domain.ValueObjects.ProductDescription model)
    {
        return new()
        {
            Name = model.Name.Value,
            SKU = model.SKU.Value,
            UnitPrice = model.UnitPrice.Value
        };
    }
}