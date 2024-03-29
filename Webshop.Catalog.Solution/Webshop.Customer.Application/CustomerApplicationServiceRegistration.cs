﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Webshop.Application;
using Webshop.Application.Contracts;

namespace Webshop.Customer.Application
{
    public static class CustomerApplicationServiceRegistration
    {
        public static IServiceCollection AddCustomerApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(opts => opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IDispatcher, Dispatcher>();

            return services;
        }
    }
}
