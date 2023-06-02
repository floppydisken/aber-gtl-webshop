using Webshop.Application.Contracts;
using Webshop.Order.Domain;
using Webshop.Order.Domain.Dto;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Abstractions;

public class BuyOrderLine
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class BuyCommand : ICommand
{
    public required int OrderId { get; set; }
    public required int CustomerId { get; set; }
    public Guid TransactionId { get; set; }
    public IEnumerable<BuyOrderLine> OrderLines { get; set; } =
        Enumerable.Empty<BuyOrderLine>();
    public string? VoucherCode { get; set; }
}
