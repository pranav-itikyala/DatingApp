
namespace API.Controllers;

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController(DataContext context,iTokenService tokenService) : BaseApiController
{
    [HttpPost("register")]//account/register

    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

        if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            return Ok();
      
        // using  var hmac=new HMACSHA256();
        // var user = new AppUser
        // {
        //     UserName = registerDto.Username.ToLower(),
        //     PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //     PasswordSalt=hmac.Key
        // };
        // context.Users.Add(user);
        // await context.SaveChangesAsync();
        // return new UserDto
        // {
        //     Username = user.UserName,
        //     Token=tokenService.CreateToken(user)
            
        // };


        
    }

    [HttpPost("login")]

    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        int i;
        var user=await context.Users
        .Include(p=>p.Photos)
        .FirstOrDefaultAsync(x => x.UserName==loginDto.UserName.ToLower());
        if (user==null) return Unauthorized("Invalid username or password");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for(i=0;i<computedHash.Length;i++)
        {
            if(computedHash[i]!=user.PasswordHash[i])
            {
                return Unauthorized("Invalid Password");
            }
           
        }
         return new UserDto
         {
            Username = user.UserName,
            Token=tokenService.CreateToken(user),
            PhotoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url

         };
    }

    private async Task<bool> UserExists (string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower()==username.ToLower());
    }

}

