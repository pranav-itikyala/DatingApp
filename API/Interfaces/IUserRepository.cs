﻿﻿using API.Entities;


namespace API.Interfaces;

public interface IUserRepository
{
    // anything that returns a reference type that is not a list make optional
    // and handle this in the controller
    void Update(AppUser user);
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto?> GetMemberAsync(string username);
    Task<bool> SaveAllAsync();

}