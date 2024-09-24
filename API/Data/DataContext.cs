﻿using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions<DataContext> options):DbContext(options)
{
    
    public DbSet<AppUser> Users { get; set; }
    public DbSet<UserLike> Likes { get; set; }
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserLike>()
            .HasKey(k=>new {k.SourceUserId,k.TargetUserId} );
            

        builder.Entity<UserLike>()
            .HasOne(s=> s.SourceUser)
            .WithMany(l => l.LikedUser)
            .HasForeignKey(s=>s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);



        builder.Entity<UserLike>()
            .HasOne(s => s.TargetUser)
            .WithMany(l => l.LikedByUsers)
            .HasForeignKey(s => s.TargetUserId)
            .OnDelete(DeleteBehavior.Cascade);

       
    }
}