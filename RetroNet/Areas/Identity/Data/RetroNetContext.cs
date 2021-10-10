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
    public class RetroNetContext : IdentityDbContext<RetroNetUser, IdentityRole, string>
    {

        public DbSet<Post> Posts { get; set; }
        public DbSet<RetroNetUser> UserList { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Blocks> Blocks { get; set; }
        public DbSet<ReportedPost> ReportedPosts {get; set;}
        public DbSet<ReportedUser> ReportedUsers { get; set; }

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
            var users = new[] {new RetroNetUser()
            {
                Id = "e1f77fa2-e9c9-4975-9e49-345fc0623f2c",
                Name = "Seth 'Rat' Richard",
                DOB = new DateTime(),
                Bio = "What it dooooo",
                UserName = "rat",
                NormalizedUserName = "RAT",
                Email = "holdenbrown10@yahoo.com",
                NormalizedEmail = "HOLDENBROWN10@YAHOO.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEEpY83n0nOjxYJXjRr7hPmLsuLbnVJzoWdhM9gLSKptvLBU3h0SgmkbmXWtW65RtYw==",
                SecurityStamp = "RQ3LPAWH7LT6NYWM2JBVNQK4YZ37DFQR",
                ConcurrencyStamp = "18cd32ec-3054-4176-96a2-24615d815da0",
                PhoneNumber = "2259317020",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0

            } };

            var roles = new[] { new IdentityRole("Admin") };

            var userRoles = new[] { new IdentityUserRole<string>() { RoleId = roles[0].Id, UserId = users[0].Id } };

            builder.Entity<RetroNetUser>().HasData(users);
            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

        }
    }
}
