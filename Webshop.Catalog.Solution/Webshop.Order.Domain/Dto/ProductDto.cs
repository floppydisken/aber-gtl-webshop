namespace Webshop.Order.Domain.Dto;

public class ProductDto : EntityDto
{
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public string Currency { get; set; } = ValueObjects.Currency.DKK.ToString();
    public int AmountInStock { get; set; } = 0;
    public int MinStock { get; set; } = 0;

    public static ProductDto Default => new ();
}