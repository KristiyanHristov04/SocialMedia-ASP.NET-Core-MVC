using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Post>()
            //    .HasMany(p => p.LikedPosts)
            //    .WithOne(lp => lp.Post)
            //    .HasForeignKey(p => p.PostId)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(au => au.LikedPosts)
                .WithOne(lp => lp.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}
