using Microsoft.IdentityModel.Tokens;
using SPW.Admin.Api.Features.Authentication.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPW.Admin.Api.Features.Authentication.Services;

public static class TokenService
{
    internal static string GenerateToken(AuthenticationEntity authenticationEntity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("DcrORx3QjaDhQTryxdEROGdkjVrzN0dt");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, authenticationEntity.Email!)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}