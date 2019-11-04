using AspNetCoreApiStarter.Bll.Itf.Bll;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreApiStarter.Bll
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddBllLibrary(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Register BLLs
            services.AddScoped<IUserBll, UserBll>();

            return services;
        }
    }
}
