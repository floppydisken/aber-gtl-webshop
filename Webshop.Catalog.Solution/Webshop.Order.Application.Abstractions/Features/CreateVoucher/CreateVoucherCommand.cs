using Webshop.Application.Contracts;

namespace Webshop.Order.Application.Abstractions;

public class CreateVoucherCommand : ICommand
{
    public string Code { get; set; }
    public decimal Amount { get; set; }
}