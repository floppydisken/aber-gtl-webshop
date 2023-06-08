using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Webshop.Application.Contracts;

namespace Webshop.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDispatcher(this IServiceCollection services)
    {
        services.AddMediatR(opts => opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
    
    public static IServiceCollection AddDispatcher(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(opts => opts.RegisterServicesFromAssembly(assembly));
        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
}
