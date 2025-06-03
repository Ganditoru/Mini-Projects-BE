using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using MiniProject.Modules.Users.Infrastructure.Authentification;

namespace MiniProject.Api.Extensions;

internal static class AuthenticationExtensions
{
    public static WebApplicationBuilder AddAuthentication(
       this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

        JwtOptions? jwtSettings = builder.Configuration
                             .GetSection("Jwt")
                             .Get<JwtOptions>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.SecretKey))
                };
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}
