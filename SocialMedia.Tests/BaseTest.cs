using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Data.Models;

namespace SocialMedia.Tests
{
    public class BaseTest
    {
        protected readonly ApplicationDbContext context;
        public BaseTest()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            this.context = new ApplicationDbContext(options);

            SeedUser();
            SeedCountries();
        }

        private void SeedUser()
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = "UserId",
                UserName = "georgi",
                NormalizedUserName = "GEORGI",
                Email = "georgi@yahoo.com",
                NormalizedEmail = "GEORGI@YAHOO.COM",
                FirstName = "Georgi",
                LastName = "Ivanov",
                CountryId = 1,
                RegistrationDate = DateTime.Now
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        private void SeedCountries()
        {
            Country country01 = new Country()
            {
                Id = 1,
                Name = "Bulgaria"
            };

            Country country02 = new Country()
            {
                Id = 2,
                Name = "Romania"
            };

            this.context.Countries.Add(country01);
            this.context.Countries.Add(country02);
            this.context.SaveChanges();
        }
    }
}