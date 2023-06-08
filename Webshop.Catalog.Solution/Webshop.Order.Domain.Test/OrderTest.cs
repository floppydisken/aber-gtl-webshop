using Webshop.Order.Domain.Entities;

namespace Webshop.Order.Domain.Test;

public class OrderTest
{
    [Fact]
    public void AggregatesTotalOfOrderLines()
    {
        var order = new AggregateRoots.Order()
        {
            OrderLines = NonEmptyEntityList.From(new []
            {
                new OrderLine()
                {
                    Product = new ProductDescription()
                    {
                        Name = NonEmptyString.From("Test Product"), 
                        SKU = NonEmptyString.From("1"), 
                        UnitPrice = Total.FromOrBoundary(10)
                    },
                    Quantity = Quantity.From(1)
                },
                new OrderLine()
                {
                    Product = new ProductDescription()
                    {
                        Name = NonEmptyString.From("Test Product"), 
                        SKU = NonEmptyString.From("1"), 
                        UnitPrice = Total.FromOrBoundary(20)
                    },
                    Quantity = Quantity.From(5)
                }
            })
        };
        
        Assert.Equal(10 + (20 * 5), order.Total.Value);
    }

    [Fact]
    public void CalculateTotalWithDiscount()
    {
        var order = new AggregateRoots.Order()
        {
            OrderLines = NonEmptyEntityList.From(new []
            {
                new OrderLine()
                {
                    Product = new ProductDescription()
                    {
                        Name = NonEmptyString.From("Test Product"), 
                        SKU = NonEmptyString.From("1"), 
                        UnitPrice = Total.FromOrBoundary(10)
                    },
                    Quantity = Quantity.From(1)
                },
                new OrderLine()
                {
                    Product = new ProductDescription()
                    {
                        Name = NonEmptyString.From("Test Product"), 
                        SKU = NonEmptyString.From("1"), 
                        UnitPrice = Total.FromOrBoundary(20)
                    },
                    Quantity = Quantity.From(5)
                }
            }),
            Discount = Discount.From(10)
        };

        var expectedTotal = (10 + (20 * 5));
        var expected = expectedTotal - (expectedTotal * 0.1m);
        
        Assert.Equal(expected, order.Total.Value);
    }
}