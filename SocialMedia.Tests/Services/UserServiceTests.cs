using Microsoft.AspNetCore.Identity;
using SocialMedia.Areas.Admin.Services;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.User;
using SocialMedia.Data.Models;

namespace SocialMedia.Tests.Services
{
    public class UserServiceTests : BaseTest
    {
        private IUserService userService;
        private ApplicationUser newUser01;
        private ApplicationUser newUser02;
        private ApplicationUser newUser03;
        private IdentityRole adminRole;
        private IdentityUserRole<string> adminUserRole;
        public UserServiceTests()
        {
            this.userService = new UserService(context);
            this.newUser01 = new ApplicationUser()
            {
                Id = "User3",
                UserName = "David",
                NormalizedUserName = "DAVID",
                Email = "david@yahoo.com",
                NormalizedEmail = "DAVID@YAHOO.COM",
                FirstName = "David",
                LastName = "Tyler",
                Country = Country01,
                RegistrationDate = DateTime.Now.AddDays(-3),
                Posts = new List<Post> { Post01 }
            };

            this.newUser02 = new ApplicationUser()
            {
                Id = "User4",
                UserName = "Amanda",
                NormalizedUserName = "AMANDA",
                Email = "amanda@yahoo.com",
                NormalizedEmail = "AMANDA@YAHOO.COM",
                FirstName = "Amanda",
                LastName = "Clark",
                Country = Country01,
                RegistrationDate = DateTime.Now.AddDays(-4),
                LikedPosts = new List<LikedPost> { LikedPost01, LikedPost02 }
            };

            this.newUser03 = new ApplicationUser()
            {
                Id = "User5",
                UserName = "Daniel",
                NormalizedUserName = "DANIEL",
                Email = "daniel@yahoo.com",
                NormalizedEmail = "DANIEL@YAHOO.COM",
                FirstName = "Daniel",
                LastName = "Wilson",
                Country = Country01,
                RegistrationDate = DateTime.Now.AddDays(-5)
            };

            this.adminRole = new IdentityRole()
            {
                Id = "IdentityRole1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            this.adminUserRole = new IdentityUserRole<string>()
            {
                RoleId = adminRole.Id,
                UserId = this.newUser03.Id
            };

            this.context.Users.Add(this.newUser01);
            this.context.Users.Add(this.newUser02);
            this.context.Users.Add(this.newUser03);
            this.context.Roles.Add(this.adminRole);
            this.context.UserRoles.Add(this.adminUserRole);
            this.context.SaveChanges();
        }

        [Fact]
        public async Task CheckIfUserEligibleForPromoteAsync_ShouldReturnFalseIfUserHasPosts()
        {
            bool result = await this.userService.CheckIfUserEligibleForPromoteAsync(this.newUser01.Id);

            Assert.False(result);
        }

        [Fact]
        public async Task CheckIfUserEligibleForPromoteAsync_ShouldReturnFalseIfUserHasLikedPosts()
        {
            bool result = await this.userService.CheckIfUserEligibleForPromoteAsync(this.newUser02.Id);

            Assert.False(result);
        }

        [Fact]
        public async Task CheckIfUserEligibleForPromoteAsync_ShouldReturnTrueIfUserDoesntHaveLikedOrPostedPosts()
        {
            bool result = await this.userService.CheckIfUserEligibleForPromoteAsync(this.newUser03.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task GetRoleByUserId_ShouldReturnRoleNameByUserId()
        {
            var roleName = await this.userService.GetRoleByUserId(this.newUser03.Id);

            Assert.Equal("Admin", roleName);
        }

        [Fact]
        public async Task GetRoleByUserId_ShouldReturnNull()
        {
            var roleName = await this.userService.GetRoleByUserId(this.newUser01.Id);

            Assert.Null(roleName);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnInformationAboutUser()
        {
            var result = await this.userService.GetUserAsync(this.newUser03.Id);

            Assert.IsType<UserViewModel>(result);
            Assert.NotNull(result);
            Assert.Equal(this.newUser03.Id, result.UserId);
            Assert.Equal(this.newUser03.UserName, result.UserUsername);
            Assert.Equal(this.newUser03.Email, result.UserEmail);
            Assert.Equal(this.newUser03.RegistrationDate.ToString("dd.MM.yyyy"), result.JoinedDate);
            Assert.Equal(this.newUser03.FirstName + " " + this.newUser03.LastName, result.UserFullName);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnCorrectDataWithoutFilter()
        {
            var usersInformation = await this.userService.GetUsersAsync(null, 1);

            var user = usersInformation.Users.First();

            Assert.Equal(User01.Id, user.UserId);
            Assert.Equal(User01.UserName, user.UserUsername);
            Assert.Equal(User01.Email, user.UserEmail);
            Assert.Equal(User01.RegistrationDate.ToString("dd.MM.yyyy"), user.JoinedDate);

            Assert.NotNull(usersInformation);
            Assert.IsType<AllViewModel>(usersInformation);
            Assert.Equal(5, usersInformation.TotalUsers);
            Assert.IsType<int>(usersInformation.TotalUsers);
            Assert.Equal(5, usersInformation.Users.Count);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnCorrectDataWithNewestFilter()
        {
            var usersInformation = await this.userService.GetUsersAsync("newest", 1);

            var user = usersInformation.Users.First();

            Assert.Equal(User01.Id, user.UserId);
            Assert.Equal(User01.UserName, user.UserUsername);
            Assert.Equal(User01.Email, user.UserEmail);
            Assert.Equal(User01.RegistrationDate.ToString("dd.MM.yyyy"), user.JoinedDate);

            Assert.NotNull(usersInformation);
            Assert.IsType<AllViewModel>(usersInformation);
            Assert.Equal(5, usersInformation.TotalUsers);
            Assert.IsType<int>(usersInformation.TotalUsers);
            Assert.Equal(5, usersInformation.Users.Count);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnCorrectDataWithOldestFilter()
        {
            var usersInformation = await this.userService.GetUsersAsync("oldest", 1);

            var user = usersInformation.Users.First();

            Assert.Equal(this.newUser03.Id, user.UserId);
            Assert.Equal(this.newUser03.UserName, user.UserUsername);
            Assert.Equal(this.newUser03.Email, user.UserEmail);
            Assert.Equal(this.newUser03.RegistrationDate.ToString("dd.MM.yyyy"), user.JoinedDate);

            Assert.NotNull(usersInformation);
            Assert.IsType<AllViewModel>(usersInformation);
            Assert.Equal(5, usersInformation.TotalUsers);
            Assert.IsType<int>(usersInformation.TotalUsers);
            Assert.Equal(5, usersInformation.Users.Count);
        }
    }
}
