using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers;

public static class IdentityExtensions
{
    
    private static JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

    
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userIdStr = claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userId = Guid.Parse(userIdStr);
        return userId;
    }

    public static string GenerateJwt(
        IEnumerable<Claim> claims,
        string key, 
        string issuer, 
        string audience, 
        int expiresInSecond
        )
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512);
        var expires = DateTime.Now.AddSeconds(expiresInSecond);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );
        return _jwtSecurityTokenHandler.WriteToken(token);
    }
}