using IceProducts.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IceProducts.Server.Extensions;

public static class IdentityServiceExtenstions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));

        var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

        var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);

        services.AddSingleton(signingConfigurations);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = signingConfigurations.SecurityKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}

public class SigningConfigurations
{
    public SecurityKey SecurityKey { get; }
    public SigningCredentials SigningCredentials { get; }
    public SigningConfigurations(string key)
    {
        var keyBytes = Encoding.ASCII.GetBytes(key);

        SecurityKey = new SymmetricSecurityKey(keyBytes);
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}
