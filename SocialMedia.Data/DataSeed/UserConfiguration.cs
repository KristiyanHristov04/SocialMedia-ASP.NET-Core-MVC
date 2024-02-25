using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data.DataSeed
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(SeedAdmin());
        }

        private ApplicationUser SeedAdmin()
        {
            PasswordHasher<ApplicationUser> passwordHasher
                = new PasswordHasher<ApplicationUser>();

            ApplicationUser admin = new ApplicationUser();

            admin.Id = "7ca7b293-0236-47cf-88d8-5165102b89ad";
            admin.UserName = "admin";
            admin.NormalizedUserName = "ADMIN";
            admin.Email = "admin@socialmedia.com";
            admin.NormalizedEmail = "ADMIN@SOCIALMEDIA.COM";
            admin.EmailConfirmed = true;
            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin123");
            admin.FirstName = "Georgi";
            admin.LastName = "Ivanov";
            admin.CountryId = 26;

            return admin;
        }
    }
}
