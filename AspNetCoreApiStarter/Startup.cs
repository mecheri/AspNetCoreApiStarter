using System.Reflection;
using AspNetCoreApiStarter.Bll;
using AspNetCoreApiStarter.Dal;
using AspNetCoreApiStarter.Middlewares;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Security;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.ViewModels.Core;
using AspNetCoreApiStarter.ViewModels.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AspNetCoreApiStarter
{
    /// <summary>
    /// Application Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">Hosting environment.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            this.Configuration = builder.Build();

            // Configure the Serilog pipeline
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.Configuration)
                .CreateLogger();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Collection of services descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register App configuration on the DI container (db connection,...)
            services.Configure<AppConfig>(this.Configuration);

            // Register BLLs
            services.AddBllLibrary();

            // Register DALs
            services.AddDalLibrary();

            // Register localizer
            services.AddResourcesLibrary();

            // Register auhtentication
            services.AddSecurityLibrary(this.Configuration);

            // Register helpers library (logger,...)
            services.AddSharedLibrary(this.Configuration);

            // Add Mvc
            // FV = > find any public, non-abstract types that inherit from AbstractValidator and register them with the container
            services
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

            // configure le nom des propriétés de validation
            FluentValidationConfig.Config();

            // acces au context http
            services.AddHttpContextAccessor();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application request pipeline.</param>
        /// <param name="env">Hosting environement.</param>
        /// <param name="loggerFactory">logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add Serilog to the logging pipeline
            loggerFactory.AddSerilog();

            // Add Localization
            app.UseRequestLocalization();

            // Enable default files
            app.UseDefaultFiles();

            // Enable static files
            app.UseStaticFiles();

            // Enable authentication middleware
            app.UseAuthentication();

            // Enabling Cross-Origin Requests (CORS)
            app.UseCors(Constants.Cors.PolicyName);

            // Enable middleware for handling errors.
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                var swaggConf = this.Configuration.GetSection(nameof(AppConfig.Swagger));
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{swaggConf[nameof(AppConfig.Swagger.Title)]} {version} (DEBUG)");
#else
                c.SwaggerEndpoint($"{swaggConf[nameof(AppConfig.Swagger.VirtualDirectory)]}/swagger/{version}/swagger.json", $"{swaggConf[nameof(AppConfig.Swagger.Title)]} {version} (RELEASE)");
#endif
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc();
        }
    }
}
