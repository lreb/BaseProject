using BaseProjectAPI.Domain.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BaseProjectAPI.Infraestructure.Extensions
{
    /// <summary>
    /// Utility to JWT
    /// </summary>
    public static class JwtExtension
    {
        /// <summary>
        /// enables JWT service
        /// </summary>
        /// <param name="services">application service <see cref="IServiceCollection"/></param>
        /// <param name="configuration">application configuration <see cref="IConfiguration"/></param>
        public static void ConfigureJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection($"{nameof(AppSettings)}:{nameof(JwtOptions)}").Get<JwtOptions>();
            
            var key = Encoding.ASCII.GetBytes(jwt.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
