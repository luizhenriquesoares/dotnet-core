using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Web.Api.Infrastructure.Guards;

namespace Web.Api.Infrastructure.Guards
{
    public static class JwtStrategyGuard
    {
        // referer : // https://github.com/renatogroffe/ASPNETCore2.2_JWT-Identity/blob/master/APIProdutos/Security/Classes.cs
        public static IServiceCollection AddJwtSecurity
            (
                this IServiceCollection services,
                SigningConfigurations signingConfigurations,
                TokenConfigurations tokenConfigurations
            )
        {
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

          
                paramsValidation.ValidateIssuer = true;
                paramsValidation.ValidateActor = true;
                paramsValidation.ValidateAudience = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ValidateIssuerSigningKey = true;
				paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            return services;

        }
    }
}
