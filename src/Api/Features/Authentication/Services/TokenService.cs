using Microsoft.IdentityModel.Tokens;
using SPW.Admin.Api.Features.Authentication.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPW.Admin.Api.Features.Authentication.Services;

public class TokenService
{
    internal ActionResult<UserToken> GenerateToken(AuthenticationEntity authenticationEntity)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, authenticationEntity.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DcrORx3QjaDhQTryxdEROGdkjVrzN0dt"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(60);

        JwtSecurityToken token = new JwtSecurityToken(
                 issuer: ("https://localhost:55539"),
                 audience: ("https://localhost:55539"),
                 claims: claims,
                 expires: expiration,
                 signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
        };
    }
}