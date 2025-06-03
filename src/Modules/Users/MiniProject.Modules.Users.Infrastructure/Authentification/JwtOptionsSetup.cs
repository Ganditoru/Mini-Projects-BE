using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MiniProject.Modules.Users.Infrastructure.Authentification;
public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _configuration;
    public JwtOptionsSetup(IConfiguration configuration)
        => _configuration = configuration;

    public void Configure(JwtOptions options)
        => _configuration.GetSection(SectionName).Bind(options);
}
