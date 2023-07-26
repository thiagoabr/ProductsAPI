using Microsoft.Extensions.DependencyInjection;
using ProductsAPI.Application.Interfaces.Services;
using ProductsAPI.Application.Interfaces.Stores;
using ProductsAPI.Application.Services;
using ProductsAPI.Domain.Interfaces.Repositories;
using ProductsAPI.Domain.Interfaces.Services;
using ProductsAPI.Domain.Services;
using ProductsAPI.Infra.Data.MongoDB.Stores;
using ProductsAPI.Infra.Data.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Infra.IoC.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IProductsAppService, ProductsAppService>();
            services.AddTransient<IProductDomainService, ProductDomainService>();
            services.AddTransient<IProductStore, ProductStore>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
