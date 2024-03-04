using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(au => au.LikedPosts)
                .WithOne(lp => lp.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .Property(au => au.CountryId)
                .IsRequired(true)
                .HasDefaultValue(26); //Bulgaria Id

            //builder.Entity<ApplicationUser>()
            //    .Property(au => au.RegistrationDate)
            //    .HasDefaultValue(DateTime.Now);

            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new StatisticConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
