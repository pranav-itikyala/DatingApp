using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API;

public class TokenService(IConfiguration config) : iTokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey=config["TokenKey"] ?? throw new Exception("Cannot access tokenkey from appsettings");
        if(tokenKey.Length < 64) throw new Exception("Your tokenKey needs to be longer");
        var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        
        var claims =new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName)
        };
        var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor=new SecurityTokenDescriptor
        {
            Subject=new ClaimsIdentity(claims),
            Expires=DateTime.UtcNow.AddDays(7),
            SigningCredentials=cred
        };

        var tokenHandler=new JwtSecurityTokenHandler();
        var token =tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
