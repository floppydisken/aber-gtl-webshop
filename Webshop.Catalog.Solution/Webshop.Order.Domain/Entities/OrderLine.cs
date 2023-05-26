using Webshop.Domain.Common;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.Entities;

public class OrderLine : Entity 
{
    public required ProductDescription Product { get; set; }
    public Quantity Quantity { get; set; }
    public Total Total { get; set; }
}
