using Webshop.Application.Contracts;

namespace Webshop.Order.Application.Abstractions;

public interface IPayForOrderCommandHandler : ICommandHandler<PayForOrderCommand>
{}