
namespace API.Controllers;

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController(DataContext context,iTokenService tokenService,
    IMapper mapper) : BaseApiController
{
    [HttpPost("register")]//account/register

    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
        using var hmac=new HMACSHA512();
        var user = mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();
        user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        user.PasswordSalt=hmac.Key;
        

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender,
            PhotoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url
        };
    }

    private async Task<bool> UserExists (string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower()==username.ToLower());
    }

}

