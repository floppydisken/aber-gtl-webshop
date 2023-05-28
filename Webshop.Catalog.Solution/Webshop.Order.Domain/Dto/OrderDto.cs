namespace Webshop.Order.Domain.Dto;

public class OrderDto : EntityDto
{
    public int CustomerId { get; set; }
    public decimal Discount { get; set; }
    public IEnumerable<OrderLineDto> OrderLines { get; set; }
}