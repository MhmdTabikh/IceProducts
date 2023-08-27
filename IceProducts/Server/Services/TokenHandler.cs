using IceProducts.Server.Data;
using IceProducts.Server.Entities;
using IceProducts.Server.Extensions;
using IceProducts.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TokenOptions = IceProducts.Server.Data.TokenOptions;

namespace IceProducts.Server.Services;


public interface ITokenHandler
{
    AccessToken CreateAccessToken(User user);
}

public class TokenHandler : ITokenHandler
{
    private readonly TokenOptions _tokenOptions;
    private readonly SigningConfigurations _signingConfigurations;
    

    public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations)
    {
        _tokenOptions = tokenOptionsSnapshot.Value;
        _signingConfigurations = signingConfigurations;
    }

    public AccessToken CreateAccessToken(User user)
    {
        var accessToken = BuildAccessToken(user);

        return accessToken;
    }

    private AccessToken BuildAccessToken(User user)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

        var securityToken = new JwtSecurityToken
        (
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: GetClaims(user),
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: _signingConfigurations.SigningCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var accessToken = handler.WriteToken(securityToken);

        return new AccessToken(accessToken, accessTokenExpiration.Ticks);
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };
        return claims;
    }
}

