using Webshop.Application.Contracts;

namespace Webshop.Order.Application.Abstractions;

public class DeleteVoucherCommand : ICommand
{
    public string Code { get; set; }
}