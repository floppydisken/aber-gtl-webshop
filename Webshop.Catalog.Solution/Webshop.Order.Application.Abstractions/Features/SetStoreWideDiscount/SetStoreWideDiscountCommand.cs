using Webshop.Application.Contracts;

namespace Webshop.Order.Application.Abstractions;

public class SetStoreWideDiscountCommand : ICommand
{
    public decimal Amount { get; set; }
}