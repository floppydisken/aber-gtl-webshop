namespace Webshop.Order.Domain.Dto;

public class VoucherDto : EntityDto
{
    public string Code { get; set; } = string.Empty;
    public decimal Discount { get; set; }
}