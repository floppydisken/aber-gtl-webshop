namespace Webshop.Order.Domain.Dto;

public class ProductDto
{
    public string SKU { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; } = 0;
}