using Webshop.Application.Contracts;

namespace Webshop.Order.Application.Abstractions;

public class PayForOrderCommand : ICommand
{
    public int OrderId { get; set; }

    public int Amount { get; set; }
    public int CVC { get; set; }
    public string CardNumber { get; set; } = "";
    public string ExpirationDate { get; set; } = "";
}