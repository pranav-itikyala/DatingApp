using API.Data;
using API.Entities;
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

    public UsersController(IUserRepository userRepository)
 {
   this.userRepository=userRepository; 
   
 }



 [HttpGet]
 public async  Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
 {
    var users = await userRepository.GetMembersAsync();

    return Ok(users);
 }



 [HttpGet ("{UserName}")]

 public async Task<ActionResult<MemberDto>> GetUsers(string username)
 {

   var user = await userRepository.GetMemberAsync(username);
   if(user==null) return NotFound();
   return user;
 }



}
