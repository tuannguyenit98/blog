﻿using Common.Helpers;
using Common.Runtime.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Infrastructure.ContainerConfigs
{
    public static class AuthServicesInstaller
    {
        private const string SecretKey = "15tuankakeyaccessjwttobuildertoken11"; // todo: get this from somewhere secure

        private static readonly SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public static void ConfigureServicesAuth(IServiceCollection services, IConfiguration configuration)
        {
            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var cloudinarySettings = configuration.GetSection(nameof(CloudinarySettings));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(
                options =>
                {
                    options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                    options.SecretKeyAccess = jwtAppSettingOptions[nameof(JwtIssuerOptions.SecretKeyAccess)];
                    options.SecretKeyRefresh = jwtAppSettingOptions[nameof(JwtIssuerOptions.SecretKeyRefresh)];
                    options.MobileExpirationInHours = jwtAppSettingOptions[nameof(JwtIssuerOptions.MobileExpirationInHours)].ParseInt();
                    options.WebExpirationInHours = jwtAppSettingOptions[nameof(JwtIssuerOptions.WebExpirationInHours)].ParseInt();
                    options.ErpExpirationInHours = jwtAppSettingOptions[nameof(JwtIssuerOptions.ErpExpirationInHours)].ParseInt();
                    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                });

            services.Configure<CloudinarySettings>(cloudinarySettings);

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
                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(
                    configureOptions =>
                    {
                        configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                        configureOptions.TokenValidationParameters = tokenValidationParameters;
                        configureOptions.SaveToken = true;
                    });

        }
    }
}
