using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Dal;
using AspNetCoreApiStarter.Dal.Base;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreApiStarter.Dal
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDalLibrary(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Register DALs
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();

            return services;
        }
    }
}
