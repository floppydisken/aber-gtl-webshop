using Webshop.Application.Contracts;
using Webshop.Order.Domain;

namespace Webshop.Order.Application.Abstractions;

public class BuyCommand : ICommand
{
    public required int CustomerId { get; set; }
    public required OrderLines OrderLines { get; set; }
}
