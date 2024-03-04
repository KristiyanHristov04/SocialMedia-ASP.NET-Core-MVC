using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data.DataSeed
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(SeedAdmin());
            builder.HasData(SeedSuperAdmin());
        }

        private IdentityUserRole<string> SeedAdmin()
        {
            IdentityUserRole<string> role = new IdentityUserRole<string>()
            {
                UserId = "7ca7b293-0236-47cf-88d8-5165102b89ad",
                RoleId = "22a1d95e-a505-4934-9f76-178c3798d64a"
            };

            return role;
        }

        private IdentityUserRole<string> SeedSuperAdmin()
        {
            IdentityUserRole<string> role = new IdentityUserRole<string>()
            {
                UserId = "7ca7b293-0236-47cf-88d8-5165102b89ad",
                RoleId = "2de99791-ebc7-452d-b2d1-c4ea8a548136"
            };

            return role;
        }
    }
}
