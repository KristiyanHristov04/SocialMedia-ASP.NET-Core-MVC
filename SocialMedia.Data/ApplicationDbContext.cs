﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SocialMedia.Data.DataSeed;
using SocialMedia.Data.Models;
using System.Reflection.Emit;

namespace SocialMedia.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<LikedPost> LikedPosts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ReportPost> ReportPosts { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(au => au.LikedPosts)
                .WithOne(lp => lp.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(au => au.Announcements)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ApplicationUser>()
                .Property(au => au.CountryId)
                .IsRequired(true)
                .HasDefaultValue(26); //Bulgaria Id

            //builder.Entity<ApplicationUser>()
            //    .Property(au => au.RegistrationDate)
            //    .HasDefaultValue(DateTime.Now);

            builder.Entity<ApplicationUser>()
                .Property(au => au.UserName)
                .IsRequired(true);

            builder.Entity<ApplicationUser>()
                .Property(au => au.NormalizedUserName)
                .IsRequired(true);

            builder.Entity<ApplicationUser>()
                .Property(au => au.Email)
                .IsRequired(true);

            builder.Entity<ApplicationUser>()
                .Property(au => au.NormalizedEmail)
                .IsRequired(true);

            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new StatisticConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
