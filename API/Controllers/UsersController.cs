using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
 

 private readonly DataContext _context;
 public readonly IUserRepository userRepository;
 
 public readonly IMapper mapper;
  public readonly  IPhotoService photoService;

    public UsersController(IUserRepository userRepository,IMapper mapper,IPhotoService photoService)
 {
   this.userRepository=userRepository; 
   this.mapper=mapper;
   this.photoService=photoService;
   
 }



 [HttpGet]
 public async  Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
 {
    userParams.CurrentUsername=User.GetUsername();
    var users = await userRepository.GetMembersAsync(userParams);

    Response.AddPaginationHeader(users);
      
    return Ok(users);
 }



 [HttpGet ("{UserName}")]

 public async Task<ActionResult<MemberDto>> GetUsers(string username)
 {

   var user = await userRepository.GetMemberAsync(username);
   if(user==null) return NotFound();
   return user;
 }

  

   [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        // var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // if(username==null) return BadRequest("No username found in token");
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());
        if(user==null) return BadRequest("Could not find user");
        mapper.Map(memberUpdateDto,user);
        if(await userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update the user");
    }

     [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Could not find user");

        var result = await photoService.AddPhotoAsync(file);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if (user.Photos.Count == 0) photo.IsMain = true;

        user.Photos.Add(photo);

        if(await userRepository.SaveAllAsync()) 
        
        return CreatedAtAction(nameof(GetUsers),new {username=user.UserName},mapper.Map<PhotoDto>(photo));


        return BadRequest("Problem adding photo");
    }

       [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Could not find user");

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null) return NotFound();

        if (photo.IsMain) return BadRequest("This is already your main photo");

        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
        if (currentMain != null) currentMain.IsMain = false;
        photo.IsMain = true;

        if (await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Problem setting main photo");
    }
  
  [HttpDelete("delete-photo/{photoId:int}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Could not find user");

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null) return NotFound();

        if (photo.IsMain) return BadRequest("You cannot delete your main photo");

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }

        user.Photos.Remove(photo);

        if (await userRepository.SaveAllAsync()) return Ok();

        return BadRequest("Problem deleting photo");
    }
  
}
