using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _90sTest.Areas.Identity.Data;
using _90sTest.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _90sTest.Data
{
    public class RetroNetContext : IdentityDbContext<RetroNetUser>
    {

        public DbSet<Post> Posts { get; set; }
        public DbSet<RetroNetUser> UserList { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Blocks> Blocks { get; set; }


        public RetroNetContext(DbContextOptions options)
            : base(options)
        {
        }

        public RetroNetContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(256));

            builder.Entity<Likes>()
                .HasKey(l => new { l.LikerId, l.LikedPostId });
            builder.Entity<Likes>()
                .HasOne(f => f.Liker)
                .WithMany(u => u.LikedPosts)
                .HasForeignKey(f => f.LikerId);
            builder.Entity<Likes>()
                .HasOne(f => f.LikedPost)
                .WithMany(u => u.LikedPosts)
                .HasForeignKey(f => f.LikedPostId);

            builder.Entity<Blocks>()
                .HasKey(f => new { f.UserId, f.BlockerId });
            builder.Entity<Blocks>()
                .HasOne(f => f.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.UserId);
            builder.Entity<Blocks>()
                .HasOne(f => f.Blocker)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.BlockerId);


        }
    }
}
