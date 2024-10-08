﻿using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    public IMapper _mapper;
   

    public UserRepository(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
         
        
    }

    public async Task<MemberDto?> GetMemberAsync(string username)
    {
           return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(
                _mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
    {
       var query= _context.Users.AsQueryable();
        query = query.Where(x => x.UserName != userParams.CurrentUsername);

        if(userParams.Gender!=null)
        {
            query=query.Where(x=>x.Gender==userParams.Gender);
        }
        
        var minDob=DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge-1));
        var maxDob=DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge-1));
        query=query.Where(x=>x.DateOfBirth>=minDob&&x.DateOfBirth<=maxDob);

         query = userParams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.Created),
            _ => query.OrderByDescending(u => u.LastActive)
        };
        return await PagedList<MemberDto>.CreateAsync(query
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
                userParams.pageNumber, userParams.PageSize);


    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);

            
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users
        .Include(p => p.Photos)
        .ToListAsync();
    }

    public void Update(AppUser user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        // Save all changes made in this context to the database
        return await _context.SaveChangesAsync() > 0;
    }
}