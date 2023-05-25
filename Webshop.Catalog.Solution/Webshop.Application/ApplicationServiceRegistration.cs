using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Webshop.Application.Contracts;

namespace Webshop.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(opts => opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
}
