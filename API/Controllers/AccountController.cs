
namespace API.Controllers;

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController(UserManager<AppUser> userManager,iTokenService tokenService,
    IMapper mapper) : BaseApiController
{
    [HttpPost("register")]//account/register

    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
        using var hmac=new HMACSHA512();
        var user = mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();
        // user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        // user.PasswordSalt=hmac.Key;
        

        var result= await userManager.CreateAsync(user,registerDto.Password);

        if(!result.Succeeded) return BadRequest(result.Errors);


        return new UserDto
        {
            Username = user.UserName,
            Token =  await tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender,
            PhotoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url
        };
    }
  [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var  user = await userManager.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.NormalizedUserName == loginDto.UserName.ToUpper());
        

        if (user == null || user.UserName==null) return Unauthorized("Invalid username");

        var result=await userManager.CheckPasswordAsync(user, loginDto.Password);

        if(!result) return Unauthorized();

        return new UserDto
        {
            Username = user.UserName,
            Token = await tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };
    }
    private async Task<bool> UserExists (string username)
    {
        return await userManager.Users.AnyAsync(x => x.NormalizedUserName==username.ToUpper());
    }

}

