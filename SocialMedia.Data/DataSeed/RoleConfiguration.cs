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
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(SeedRoles());
        }

        private List<IdentityRole> SeedRoles()
        {
            List<IdentityRole> roles = new List<IdentityRole>();

            IdentityRole adminRole = new IdentityRole();
            adminRole.Id = "22a1d95e-a505-4934-9f76-178c3798d64a";
            adminRole.Name = "Administrator";
            adminRole.NormalizedName = "ADMINISTRATOR";

            IdentityRole userRole = new IdentityRole();
            userRole.Id = "23fd43c7-113f-4b97-8d21-6cd8661b96b1";
            userRole.Name = "User";
            userRole.NormalizedName = "USER";

            roles.Add(adminRole);
            roles.Add(userRole);

            return roles;
        }
    }
}
