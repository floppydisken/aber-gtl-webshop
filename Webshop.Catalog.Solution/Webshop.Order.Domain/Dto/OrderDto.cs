using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.Dto;

public class OrderDto : EntityDto
{
    public int CustomerId { get; set; }
    public Guid? TransactionId { get; set; }
    public string Status { get; set; } = OrderStatus.Created.Value;
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public IEnumerable<OrderLineDto> OrderLines { get; set; } = Enumerable.Empty<OrderLineDto>();
}