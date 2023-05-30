namespace Webshop.Order.Domain.Dto;

public class ProductDto : EntityDto
{
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; } = 0;

    public static ProductDto Default => new ();
}