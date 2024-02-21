using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SocialMedia.Data.DataSeed;
using SocialMedia.Data.Models;

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

            builder.ApplyConfiguration(new CountryConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
