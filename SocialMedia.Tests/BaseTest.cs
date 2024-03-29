using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Data.Models;

namespace SocialMedia.Tests
{
    public abstract class BaseTest
    {
        protected readonly ApplicationDbContext context;
        protected BaseTest()
        {
            DbContextOptions<ApplicationDbContext> options
                = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            this.context = new ApplicationDbContext(options);

            SeedCountries();
            SeedUsers();
            SeedPosts();
            SeedLikedPosts();
            SeedReportedPost();
            SeedStatistic();
        }
        protected Country Country01 { get; private set; }
        protected Country Country02 { get; private set; }
        protected ApplicationUser User01 { get; private set; }
        protected ApplicationUser User02 { get; private set; }
        protected Post Post01 { get; private set; }
        protected Post Post02 { get; private set; }
        protected LikedPost LikedPost01 { get; private set; }
        protected LikedPost LikedPost02 { get; private set; }
        protected ReportPost ReportPost01 { get; private set; }

        private void SeedCountries()
        {
            Country01 = new Country()
            {
                Id = 1,
                Name = "Bulgaria"
            };

            Country02 = new Country()
            {
                Id = 2,
                Name = "Romania"
            };

            this.context.Countries.Add(Country01);
            this.context.Countries.Add(Country02);
            this.context.SaveChanges();
        }

        private void SeedUsers()
        {
            User01 = new ApplicationUser()
            {
                Id = "User1",
                UserName = "John",
                NormalizedUserName = "John",
                Email = "john@yahoo.com",
                NormalizedEmail = "John@YAHOO.COM",
                FirstName = "John",
                LastName = "Doe",
                Country = Country01,
                RegistrationDate = DateTime.Now
            };

            User02 = new ApplicationUser()
            {
                Id = "User2",
                UserName = "Jane",
                NormalizedUserName = "JANE",
                Email = "jane@yahoo.com",
                NormalizedEmail = "JANE@YAHOO.COM",
                FirstName = "Jane",
                LastName = "Smith",
                Country = Country02,
                RegistrationDate = DateTime.Now
            };

            this.context.Users.Add(User01);
            this.context.Users.Add(User02);
            this.context.SaveChanges();
        }

        private void SeedPosts()
        {
            Post01 = new Post()
            {
                Id = 1,
                Text = "Post1",
                Path = "somepath",
                User = User01,
                Date = DateTime.Now
            };

            Post02 = new Post()
            {
                Id = 2,
                Text = "Post2",
                Path = "somepath",
                User = User02,
                Date = DateTime.Now
            };

            this.context.Posts.Add(Post01);
            this.context.Posts.Add(Post02);
            this.context.SaveChanges();
        }

        private void SeedLikedPosts()
        {
            LikedPost01 = new LikedPost()
            {
                Id = 1,
                Post = Post01,
                User = User01
            };

            LikedPost02 = new LikedPost()
            {
                Id = 2,
                Post = Post02,
                User = User02
            };

            this.context.LikedPosts.Add(LikedPost01);
            this.context.LikedPosts.Add(LikedPost02);
            this.context.SaveChanges();
        }

        private void SeedReportedPost()
        {
            ReportPost01 = new ReportPost()
            {
                Id = 1,
                Post = Post02,
                ReportsCount = 1
            };

            this.context.ReportPosts.Add(ReportPost01);
            this.context.SaveChanges();
        }

        private void SeedStatistic()
        {
            Statistic statistic = new Statistic()
            {
                Id = 1,
                AllTimeUsersCount = 0,
                ReportedPostsDeletedCount = 0
            };

            this.context.Statistics.Add(statistic);
            this.context.SaveChanges();
        }
    }
}