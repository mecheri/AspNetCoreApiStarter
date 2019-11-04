using System;
using System.IO;
using System.Reflection;
using AspNetCoreApiStarter.Shared.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCoreApiStarter.Shared
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddSharedLibrary(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Register Application Logger
            // services.AddSingleton<ILoggerHelper, LoggerHelper>();
            services.AddSingleton(typeof(ILoggerHelper<>), typeof(LoggerHelper<>));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                var swaggConf = Configuration.GetSection(nameof(AppConfig.Swagger));
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                c.SwaggerDoc(version, new Info
                {
                    Title = swaggConf[nameof(AppConfig.Swagger.Title)],
                    Version = $"v{version}",
                    TermsOfService = "None",
                    Contact = new Contact {
                        Name = swaggConf[nameof(AppConfig.Swagger.ContactName)],
                        Email = swaggConf[nameof(AppConfig.Swagger.ContactEmail)],
                        Url = swaggConf[nameof(AppConfig.Swagger.ContactUrl)]
                    }
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey",
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = System.AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, $"{Configuration[nameof(AppConfig.AppName)]}.xml");
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
