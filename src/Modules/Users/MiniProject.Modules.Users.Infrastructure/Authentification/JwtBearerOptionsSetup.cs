using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MiniProject.Modules.Users.Infrastructure.Authentification;
public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{

    public void Configure(JwtBearerOptions options)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes("your_super_duper_huper_secret_key");
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        };

    }
}
