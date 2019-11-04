using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Security.Auth;
using AspNetCoreApiStarter.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace AspNetCoreApiStarter.Security
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddSecurityLibrary(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // injecter le contexte utilisateur
            services.AddTransient<UserCtxResolver>();

            // Register JWT factory
            services.AddSingleton<IJwtFactory, JwtFactory>();

            // Jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingOptions[nameof(JwtIssuerOptions.SecretKey)]));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

                // parce que Microsoft JWT handler that turns these standard claims into Microsoft proprietary ones
                configureOptions.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().First().InboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = JwtRegisteredClaimNames.Sub;
            });

            // Api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));
            });

            // Enabling Cross-Origin Requests (CORS)
            services.AddCors(options =>
            {
                string[] origins = Configuration[nameof(AppConfig.AllowedOrigins)].Split(",");
                options.AddPolicy(
                    Constants.Cors.PolicyName, b => b.WithOrigins(origins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
            });

            return services;
        }
    }
}
