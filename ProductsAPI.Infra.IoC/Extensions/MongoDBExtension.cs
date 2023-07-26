using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsAPI.Infra.Data.MongoDB.Contexts;
using ProductsAPI.Infra.Data.MongoDB.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Infra.IoC.Extensions
{
    public static class MongoDBExtension
    {
        public static IServiceCollection AddMongoDBConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDBSettings"));
            services.AddTransient<MongoDBContext>();

            return services;
        }
    }
}
