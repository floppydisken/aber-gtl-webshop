namespace Webshop.Order.Domain.Dto;

public class VoucherDto : EntityDto
{
    public string Code { get; set; }
    public decimal Discount { get; set; }
}