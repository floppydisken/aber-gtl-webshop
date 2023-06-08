namespace Webshop.Order.Domain.Dto;

public class OrderLineDto : EntityDto
{
    public ProductDto Product { get; set; } = ProductDto.Default;
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}