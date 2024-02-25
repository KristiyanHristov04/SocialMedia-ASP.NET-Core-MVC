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
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            //Seeding a  'Administrator' role to AspNetRoles table
            //builder.Entity<IdentityRole>()
            //    .HasData(new IdentityRole
            //    {
            //        Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            //        Name = "Administrator",
            //        NormalizedName = "ADMINISTRATOR".ToUpper()
            //    });


            ////a hasher to hash the password before seeding the user to the db
            //var hasher = new PasswordHasher<IdentityUser>();

            ////Seeding the User to AspNetUsers table
            //builder.Entity<IdentityUser>()
            //    .HasData(new IdentityUser
            //    {
            //        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
            //        UserName = "myuser",
            //        NormalizedUserName = "MYUSER",
            //        PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
            //    }
            //);


            ////Seeding the relation between our user and role to AspNetUserRoles table
            //builder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>
            //    {
            //        RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            //        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            //    }
            //);

            base.OnModelCreating(builder);
        }
    }
}
